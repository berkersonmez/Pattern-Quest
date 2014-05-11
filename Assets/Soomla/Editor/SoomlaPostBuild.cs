using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System;

public class PostProcessScriptStarter : MonoBehaviour {
	
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {

		string buildToolsDir = Application.dataPath + DSC + @"Soomla" + DSC + @"Editor" + DSC + @"build-tools";
#if UNITY_ANDROID
		string fileName = Path.GetFileName(pathToBuiltProject);
		string dirName = Path.GetDirectoryName(pathToBuiltProject);

		System.IO.File.Copy(buildToolsDir + DSC + @"apktool.jar", dirName + DSC + @"apktool.jar", true);
		
		runProcess(@"java", @"-jar apktool.jar d -f " + fileName + @" repack_apk", dirName, null);
		
		copyDirectory(Application.dataPath + DSC + @".." + DSC + @"SoomlaStorefront" + DSC, dirName + DSC + @"repack_apk" + DSC + @"assets" + DSC, true);

		// overwriting AndroidManifest.xml b/c it sometimes changes on repack
		System.IO.File.Copy(Application.dataPath + DSC + @"Plugins" + DSC + @"Android" + DSC + @"AndroidManifest.xml", dirName + DSC + @"repack_apk" + DSC + @"AndroidManifest.xml", true);

		string addToPath = EditorPrefs.GetString("AndroidSdkRoot") + DSC + @"platform-tools";
		if (!Directory.Exists(EditorPrefs.GetString("AndroidSdkRoot") + DSC + @"platform-tools")) {
			UnityEngine.Debug.Log("error: android SDK's platform-tools folder doesn't exist! You need to make sure you have the android SDK installed.");
		}

		string buildToolsPath = EditorPrefs.GetString("AndroidSdkRoot") + DSC + @"build-tools";
		if (Directory.Exists(buildToolsPath)) {
			string[] subdirs = Directory.GetDirectories(buildToolsPath);
			if (subdirs.Length > 0) {
                string subdir = subdirs[0];
                for (int i=1; i<subdirs.Length; i++){
                    Version v1 = new Version(Path.GetFileName(subdir));
                    Version v2 = new Version(Path.GetFileName(subdirs[i]));
                    if (v1.CompareTo(v2) < 0) {
                        subdir = subdirs[i];
                    }
                }

                addToPath += System.IO.Path.PathSeparator + subdir;
			}
		}

		runProcess(@"java", @"-jar apktool.jar b repack_apk", dirName, addToPath);
		
		string homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		switch(Environment.OSVersion.Platform) {
		case PlatformID.WinCE:
		case PlatformID.Win32Windows:
		case PlatformID.Win32S:
		case PlatformID.Win32NT:
			homeFolder = Directory.GetParent(homeFolder).FullName;
			break;
		default:
			break;
		}
		
		string keyStore = PlayerSettings.Android.keystoreName;
		string keyStorePass = PlayerSettings.Android.keystorePass;
		if (string.IsNullOrEmpty(keyStore)) {
			keyStore = homeFolder + DSC + @".android" + DSC + @"debug.keystore";
			keyStorePass = @"android";
		}
		string keyAlias = PlayerSettings.Android.keyaliasName;
		string keyPass = PlayerSettings.Android.keyaliasPass;
		if (string.IsNullOrEmpty(keyAlias)) {
			keyAlias = @"androiddebugkey";
			keyPass = @"android";
		}
		
		System.OperatingSystem osInfo = System.Environment.OSVersion;
		if (osInfo.Platform == PlatformID.Win32Windows ||
			osInfo.Platform == PlatformID.Win32S ||
			osInfo.Platform == PlatformID.Win32NT) {
			runProcess(@"jarsigner", 
				@"-keystore """ + keyStore + @""" -storepass """ + keyStorePass + @""" -keypass """ + keyPass + @""" ""repack_apk" + DSC + @"dist" + DSC + fileName + @""" """ + keyAlias + @"""",
				dirName, @"");			
		}
		else {
			runProcess(@"jarsigner", 
				@"-keystore '" + keyStore + @"' -storepass '" + keyStorePass + @"' -keypass '" + keyPass + @"' 'repack_apk" + DSC + @"dist" + DSC + fileName + @"' '" + keyAlias + @"'",
				dirName, @"");
		}
		
		System.IO.File.Delete(pathToBuiltProject);
		
		runProcess(EditorPrefs.GetString("AndroidSdkRoot") + DSC + @"tools" + DSC + @"zipalign", @"-v 4 repack_apk" + DSC + @"dist" + DSC + fileName + @" " + fileName, dirName, @"");
		
		System.IO.File.Delete(dirName + DSC + @"apktool.jar");
		Directory.Delete(dirName + DSC + @"repack_apk", true);
#elif UNITY_IOS
		Directory.CreateDirectory(pathToBuiltProject + @"/resources/");
		copyDirectory(Application.dataPath + @"/../SoomlaStorefront/", pathToBuiltProject + @"/resources/", true);


		Process proc = new System.Diagnostics.Process();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.CreateNoWindow = true;
		//		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.EnableRaisingEvents=false; 
		proc.StartInfo.FileName = "chmod";
		proc.StartInfo.Arguments = "755 \"" + buildToolsDir + "/PostprocessBuildPlayerScriptForSoomla\"";
		proc.Start();
		//		string output = proc.StandardOutput.ReadToEnd();
		string err = proc.StandardError.ReadToEnd();
		proc.WaitForExit();

		proc = new System.Diagnostics.Process();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.CreateNoWindow = true;
//		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.EnableRaisingEvents=false; 
		proc.StartInfo.FileName = buildToolsDir + "/PostprocessBuildPlayerScriptForSoomla";
		proc.StartInfo.Arguments = Application.dataPath.Replace(" ", "_SOOM@#") + " " + pathToBuiltProject.Replace(" ", "_SOOM@#");
		proc.Start();
//		string output = proc.StandardOutput.ReadToEnd();
		err = proc.StandardError.ReadToEnd();
		proc.WaitForExit();
//		UnityEngine.Debug.Log("out: " + output);
		if (proc.ExitCode != 0) {
			UnityEngine.Debug.Log("error: " + err + "   code: " + proc.ExitCode);
		}
#endif
    }

	private static char DSC = System.IO.Path.DirectorySeparatorChar;
	
#if UNITY_ANDROID	
	private static void runProcess (string fileName, string arguments, string workingDir, string addToPath)
	{
		System.Diagnostics.Process proc = new System.Diagnostics.Process();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.CreateNoWindow = true;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.RedirectStandardError = true;			
		proc.EnableRaisingEvents=false;
		proc.StartInfo.WorkingDirectory = workingDir;
		if (!string.IsNullOrEmpty(addToPath)) {
			proc.StartInfo.EnvironmentVariables["PATH"] = 
				proc.StartInfo.EnvironmentVariables["PATH"] + 
				System.IO.Path.PathSeparator + 
				addToPath;
			
//			UnityEngine.Debug.Log("adding to path: " + addToPath);
		}
		proc.StartInfo.FileName = fileName;
		proc.StartInfo.Arguments = arguments;
//		UnityEngine.Debug.Log("running: " + fileName + " with args: " + arguments);
		proc.Start();
		string output = proc.StandardOutput.ReadToEnd();
		string err = proc.StandardError.ReadToEnd();
		proc.WaitForExit();
//		UnityEngine.Debug.Log("out: " + output);
		if (proc.ExitCode != 0) {
			UnityEngine.Debug.LogError("error: " + err + "   code: " + proc.ExitCode);
		}
	}
#endif
	
	private static bool copyDirectory(string sourcePath, string destinationPath, bool overwriteexisting) {  
	   bool ret = false;  
	   try  
	   {
	       sourcePath = sourcePath.EndsWith(@"" + DSC) ? sourcePath : sourcePath + DSC;  
	       destinationPath = destinationPath.EndsWith(@"" + DSC) ? destinationPath : destinationPath + DSC;  

	       if (Directory.Exists(sourcePath))  
	       {
	           if (!Directory.Exists(destinationPath))  
	               Directory.CreateDirectory(destinationPath);  
	
	           foreach (string fls in Directory.GetFiles(sourcePath))  
	           {
	               FileInfo flinfo = new FileInfo(fls);  
	               flinfo.CopyTo(destinationPath + DSC + flinfo.Name, overwriteexisting);  
	           }
	           foreach (string drs in Directory.GetDirectories(sourcePath))  
	           {
	               DirectoryInfo drinfo = new DirectoryInfo(drs);  
	               if (!copyDirectory(drs, destinationPath + DSC + drinfo.Name, overwriteexisting))  
	                   ret = false;
	           }

	       }
	       ret = true;  
	   }
	   catch
	   {
	       ret = false;  
	   }
	   return ret;  
	}

}
