namespace Slate.KnowledgeBase.Preprocessor;

public static class ExRef
{
	private static int _exRefId = 0;

	public static string Make(string pageName, string anchorName, string exRefHandle)
	{

		var anchorStr = anchorName == null ? "null" : $"'{anchorName}'";

		return $"INSERT Into KnowledgeBaseLinks VALUES ({_exRefId++}, '{exRefHandle}','{pageName}', {anchorStr});";


	}

}