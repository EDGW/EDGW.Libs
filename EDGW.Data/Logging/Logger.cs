using EDGW.Globalization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.Data.Logging
{
    public class Logger
    {
        public Logger(string name, TextWriter writer) : this(name, null, writer)
        {

        }
        public Logger(string name, Text display) : this(name, display, null)
        {

        }
        public Logger(string name) : this(name, null, null)
        {

        }
        public Logger(string name, Text? display = null, TextWriter? writer = null)
        {
            Writer = writer ?? DefaultWriter;
            Display = display ?? name;
            Name = name;
        }
        static Logger logger = new("Logger");
        public static TextWriter DefaultWriter => LogFile ? LogFileWriter : Console.Out;
        public static TextWriter LogFileWriter { get; set; } = Console.Out;
        public static object locker = new();
        public static bool LogFile { get; private set; } = false;
        public static string LogDirPath { get; set; } = "logs\\";
        public static void OpenLogFile(string? path = null)
        {
            if (path != null) LogDirPath = path;
            var s = GetLogFileStream();
            if (s != null) {
                LogFileWriter = new StreamWriter(s);
                LogFileWriter = new BindedTextWriter(Console.Out, LogFileWriter);
                LogFile = true; 
            }
        }
        public static FileStream? GetLogFileStream()
        {
            string date = DateTime.Now.ToString(@"yy-MM-dd HH");
            int counter = 0;
            FileStream? stream = null;
            while (counter < 1000)
            {
                counter++;
                var pth = Path.Combine(LogDirPath, date + counter + ".json");
                try
                {
                    Directory.CreateDirectory(LogDirPath);
                    stream = new FileStream(pth, FileMode.OpenOrCreate, FileAccess.Write);
                    stream.SetLength(0);
                    logger.Info($"Set logger file to {pth}.");
                    break;
                }catch(Exception ex)
                {
                    logger.Error($"Cannot open log file {pth}.", ex);
                }
            }
            if (stream != null)
            {
                return stream;
            }
            else
            {
                logger.Fatal($"Cannot open log file after 1000 trials,please check your file system.");
                return null;
            }
        }
        public TextWriter Writer { get; private set; }
        public Text Display { get; set; }
        public string Name { get; private set; }
        public void SetWriter(TextWriter? writer)
        {
            lock (Writer)
            {
                Writer = writer ?? DefaultWriter;
            }
        }
        public void Info(string content)
        {
            lock (locker)
            {
                Print("INFO", content);
            }
        }
        public void Info(string content, params (string, object)[] vars)
        {
            lock (locker)
            {
                Print("INFO", content);
                PrintVariableInfo("INFO", vars);
            }
        }
        public void Warn(string content)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Yellow);
                Print("WARN", content);
                ResetColor();
            }
        }
        public void Warn(string content, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Yellow);
                Print("WARN", content);
                PrintVariableInfo("WARN", vars);
                ResetColor();
            }
        }
        public void Warn(string content, Exception ex)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Yellow);
                Print("WARN", content);
                PrintExceptionInfo("WARN", ex);
                ResetColor();
            }
        }
        public void Warn(string content, Exception ex, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Yellow);
                Print("WARN", content);
                PrintVariableInfo("WARN", vars);
                PrintExceptionInfo("WARN", ex);
                ResetColor();
            }
        }
        public void Error(string content)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Red);
                Print("ERROR", content);
                ResetColor();
            }
        }
        public void Error(string content, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Red);
                Print("ERROR", content);
                PrintVariableInfo("ERROR", vars);
                ResetColor();
            }
        }
        public void Error(string content, Exception ex)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Red);
                Print("ERROR", content);
                PrintExceptionInfo("ERROR", ex);
                ResetColor();
            }
        }
        public void Error(string content, Exception ex, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.Red);
                Print("ERROR", content);
                PrintVariableInfo("ERROR", vars);
                PrintExceptionInfo("ERROR", ex);
                ResetColor();
            }
        }
        public void Fatal(string content)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.DarkRed);
                Print("FATAL", content);
                ResetColor();
            }
        }
        public void Fatal(string content, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.DarkRed);
                Print("FATAL", content);
                PrintVariableInfo("FATAL", vars);
                ResetColor();
            }
        }
        public void Fatal(string content, Exception ex)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.DarkRed);
                Print("FATAL", content);
                PrintExceptionInfo("FATAL", ex);
                ResetColor();
            }
        }
        public void Fatal(string content, Exception ex, params (string, object)[] vars)
        {
            lock (locker)
            {
                SetColor(ConsoleColor.DarkRed);
                Print("FATAL", content);
                PrintVariableInfo("FATAL", vars);
                PrintExceptionInfo("FATAL", ex);
                ResetColor();
            }
        }
        internal string GetLine(string title,string type,string content, int tab)
        {
            return $"[{DateTime.Now} / {type.PadLeft(5)}] ({title}) {"".PadLeft(tab)}{content}";
        }
        internal void PrintLine(string title, string type, string content, int tab)
        {
            lock (Writer)
            {
                Writer.WriteLine(GetLine(title, type, content, tab));
            }
        }
        Stack<ConsoleColor> CachedColor { get; } = new();
        public void SetColor(ConsoleColor n)
        {
            CachedColor.Push(Console.ForegroundColor);
            Console.ForegroundColor = n;
        }
        public void ResetColor()
        {
            if (CachedColor.Any())
            {
                Console.ForegroundColor = CachedColor.Pop();
            }
        }
        internal void Print(string type, string lines, int tab = 0)
        {
            foreach (var line in lines.Split('\n')) PrintLine(Name, type, line, tab);
        }
        internal void PrintExceptionInfo(string type, Exception exception, int tab = 4, int time = 0)
        {
            Print(type, $"Exception Info:\n{exception}", tab);
            if (exception.InnerException != null)
            {
                if (time < 10)
                {
                    Print(type, $"Inner Exception[{time + 1}]:\n", tab + 4);
                    PrintExceptionInfo(type, exception.InnerException, tab + 4, time + 1);
                }
                else
                {
                    Print(type, $"Max Inner Exception Print Times Exceeded\nInner Exception is:\n{exception.InnerException}.", tab + 4);
                }
            }
        }
        internal void PrintVariableInfo(string type, params (string, object)[] vars)
        {
            Dictionary<string, string> vs = new();
            int mxlen = 0;
            foreach (var v in vars)
            {
                var nme = v.Item1;
                mxlen = Math.Max(nme.Length, mxlen);
                var val = v.Item2;
                try
                {
                    vs[nme] = val.ToString() ?? "{null}";
                }
                catch (Exception ex)
                {
                    vs[nme] = ex.GetType().ToString();
                }
            }
            if (mxlen % 4 != 0)
            {
                mxlen = (mxlen / 4 + 1) * 4;
            }
            StringBuilder sb = new();
            sb.Append($"Related variables({vs.Count}):\n");
            foreach(var v in vs)
            {
                sb.Append($"{v.Key.PadRight(mxlen)}:{v.Value}\n");
            }
            if (sb.Length >= 1) sb.Remove(sb.Length - 1, 1);
            Print(type, sb.ToString(), 4);
        }
    }
}
