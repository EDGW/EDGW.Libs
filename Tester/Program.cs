using EDGW.Data.Logging;

Logger lger = new("DEBUG");

lger.Info("this is an info.");
lger.Warn("this is a  warn.");
lger.Error("this is an error.");
lger.Fatal("this is a  fatal.");
lger.Fatal("test variables & exceptions", new NullReferenceException(), ("aaa", "this is a string"), ("ALIGNMENT TEST", "string"), ("OBJECT TST", new object()));

Console.ReadLine();