using System.Web.Optimization;

namespace InBed.Web
{
    public class InBedScriptBundle : ScriptBundle
    {
        readonly JavascriptObfuscator _jso = new JavascriptObfuscator();

        public InBedScriptBundle(string virtrualPath)
            : base(virtrualPath)
        {
            Transforms.Add(_jso);
        }
    }
}