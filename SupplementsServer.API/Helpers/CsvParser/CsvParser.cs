using System.Diagnostics;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace SupplementsServer.API.Helpers.CsvParser; 

public class CsvParser {
    private string _pathToFile;
    public CsvParser(string pathToCsvFile) {
        _pathToFile = pathToCsvFile;
    }
    /// <summary>
    /// Parses CSV file and returns its content in form of List of CsvResults.
    /// </summary>
    /// <param name="pathToCsvFile">If you want to parse another file then from constructor.</param>
    /// <returns>List of parsed rows as CsvResults.</returns>
    /// <exception cref="Exception">If the file can't be parsed.</exception>
    public async Task<List<CsvResult>> Parse(string pathToCsvFile = "") {
        var result =  new List<CsvResult>();
        string path = string.IsNullOrEmpty(pathToCsvFile) ? _pathToFile : pathToCsvFile;
        
        TextFieldParser tfp = new TextFieldParser(path, Encoding.UTF8);
        tfp.TextFieldType = FieldType.Delimited;
        tfp.Delimiters = new[] {","};
        
        string[] headers = tfp.ReadFields();
        if (headers == null)
            throw new Exception("Couldn't load content of the CSV file.");
        
        List<string[]> dataRows = new List<string[]>();
        
        while (!tfp.EndOfData) {
            string[] fields = tfp.ReadFields();
            if(fields == null) continue;
            dataRows.Add(fields);
        }

        return await ParseRows(headers, dataRows);
    }

    /// <summary>
    /// Make out of data rows CsvResults objects that uses keys from headers array.
    /// </summary>
    /// <param name="headers">Keys for values in dataRows.</param>
    /// <param name="dataRows">List of rows from CSV file.</param>
    /// <returns></returns>
    private async Task<List<CsvResult>> ParseRows(string[] headers, List<string[]> dataRows) {
        const int MAX_EVENTS = 64;
        
        List<CsvResult> results = new List<CsvResult>();
        int wholeIterations = (int)Math.Floor((float)dataRows.Count / MAX_EVENTS);
        int lastIterRows = dataRows.Count % MAX_EVENTS;
        for (int j = 0; j < wholeIterations; j++) {
            await RunParseThreads(headers, dataRows, results, MAX_EVENTS, j * MAX_EVENTS);
        }

        await RunParseThreads(headers, dataRows, results, lastIterRows, wholeIterations * MAX_EVENTS);
        

        return results;
    }

    /// <summary>
    /// Runs set amount of threads that will parse data rows from the set index forwards to the limit.
    /// </summary>
    /// <param name="headers">Keys for values.</param>
    /// <param name="dataRows">List of rows from CSV file.</param>
    /// <param name="results">Output list for created CsvResults.</param>
    /// <param name="countThreads">Limit of usable threads.</param>
    /// <param name="dataRowsStartIndex">Index from where the function will read new data rows.</param>
    private async Task RunParseThreads(string[] headers, List<string[]> dataRows, List<CsvResult> results, int countThreads,
                                 int dataRowsStartIndex) {
        ManualResetEvent[] waitHandlers = new ManualResetEvent[countThreads];
        List<Thread> threads = new List<Thread>();
        
        for (int i = 0; i < countThreads; i++) {
            if (dataRows[i+dataRowsStartIndex].Length != headers.Length) {
                Debugger.Log(0,"",$"Skipped dataRow with index = {i}");
                continue;
            }
            ManualResetEvent waitHandler = new ManualResetEvent(false);
            waitHandlers[i] = waitHandler;

            Thread thread = new Thread(() => ThreadParseDataRowFunc(headers, dataRows[i], results, waitHandler));
            threads.Add(thread);
            thread.Start();
        }

        WaitHandle.WaitAll(waitHandlers);

        foreach (Thread thread in threads) {
            thread.Join();
        }
    }
    
    /// <summary>
    /// Function for the Thread that will parse one from CSV file.
    /// </summary>
    /// <param name="headers">Keys for values.</param>
    /// <param name="dataRow">Values.</param>
    /// <param name="results">Output list.</param>
    /// <param name="waitHandler">Wait handler for finish signalization.</param>
    private static void ThreadParseDataRowFunc(string[] headers, string[] dataRow, List<CsvResult> results, ManualResetEvent waitHandler) {
        try {
            CsvResult result = new CsvResult();
            for (int i = 0; i < headers.Length; i++) {
                result.AddValue(headers[i], dataRow[i]);
            }

            lock (results) {
                results.Add(result);
            }
            
        }
        finally {
            waitHandler.Set();
        }
    }
}