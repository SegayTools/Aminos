namespace Aminos.Kernels.Files
{
	public interface IApplicationFilePath
	{
		public string TempFolderPath { get; }
		public string ApplicationDataFolderPath { get; }
	}
}
