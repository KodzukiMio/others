using System;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;
using Terraria;

namespace KUR
{
    public class V8 : BaseClass<V8>
    {
        public bool input_flag = true;
        public V8ScriptEngine engine = null;
        public ScriptC scriptC = null;
        public V8()
        {
            this.engine = new V8ScriptEngine();
            this.scriptC = new ScriptC(this.engine, this);
            Load();
        }
        public void Load()
        {
            engine.AddHostObject("SYS", this.scriptC);
            engine.AddHostType(typeof(JsonConvert));
            engine.AddHostObject("LIB", new Microsoft.ClearScript.HostTypeCollection("mscorlib", "System.Core"));
            engine.Execute(@"const Cout =function (value) {if (typeof value === 'function') {value = value.toString(); } else {value = SYS.SerializeObject(value);}SYS.WriteLine(value);};");
            engine.Execute(@"const _ = (obj) => {Cout(typeof(obj));let properties = new Set();let currentObj = obj;do {Object.getOwnPropertyNames(currentObj).map(item => properties.add(item));} while ((currentObj = Object.getPrototypeOf(currentObj)))return [...properties.keys()];}");
            engine.Execute(@"const Console=LIB.Main.NewText;");
            dynamic dynamicResult = engine.Evaluate("this");
            foreach (var name in dynamicResult.GetDynamicMemberNames())
                Main.NewText("{0}: {1}", name, dynamicResult[name]);
        }
        public void SetInputFlag(bool _flag) { this.input_flag = _flag; }
        public string Eval(ref string str)
        {
            try
            {
                var result = engine.Evaluate(str);
                if (result != null)
                {
                    if (result.ToString() == "[undefined]")
                    {
                        Main.NewText("[undefined]");
                        return ("");
                    };
                    var serializedResult = JsonConvert.SerializeObject(result);
                    return serializedResult;
                }
            } catch (Exception)
            {
                try
                {
                    Main.NewText(JsonConvert.SerializeObject(engine.Evaluate("_(" + str + ");")));
                } catch (Exception ex_)
                {
                    return ex_.Message;
                }
            }
            return ("____________");
        }

    };
    public class ScriptC
    {
        public V8ScriptEngine engine = null;
        public V8 v8 = null;
        public ScriptC(V8ScriptEngine _engine, V8 v8_)
        {
            this.engine = _engine;
            this.v8 = v8_;
        }
        public string SerializeObject(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            } catch (Exception ex)
            {
                return ex.Message;
            };
        }
        public void Global()
        {
            var propertyNames = engine.Global.PropertyNames.GetEnumerator();
            while (propertyNames.MoveNext())
            {
                string element = propertyNames.Current;
                string gettype = ("(typeof " + element + ")");
                Main.NewText($"{v8.Eval(ref gettype)} :'{element}'");
            }
            propertyNames.Dispose();
        }
        public dynamic Window() { return engine.Script; }
        public void WriteLine(string value)
        {
            Main.NewText(value);
        }
        public void EXIT() { v8.SetInputFlag(false); }
    }
    public abstract class BaseClass<T> where T : BaseClass<T>, new()
    {//静态创建
        public static T Create(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
        public static T Create()
        {
            return new T();
        }
    };

}

