using UnityEditor;
using UnityEditor.Compilation;

namespace Odumbrata.Editor
{
    public class CompilationEditor
    {
        [MenuItem(EditorPaths.Recompile)]
        public static void Recompile()
        {
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}