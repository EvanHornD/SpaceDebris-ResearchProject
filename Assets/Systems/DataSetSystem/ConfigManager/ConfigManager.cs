using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class ConfigManager
{

	private string configFilePath;
	private Dictionary<string, int> indexMap;
	private List<Config> configs;

	public ConfigManager(string configFilePath)
	{
		this.configFilePath = configFilePath;
		initConfig();
	}

	#region initialization
	public void initConfig()
	{
		if (!File.Exists(configFilePath)) 
		{
            Debug.Log("Config File not found at: " + configFilePath);
			createFile();
            indexMap = new Dictionary<string, int>();
			return;
        }
		using (StreamReader configReader = new StreamReader(configFilePath))
		{
            indexFile(configReader);
		}
	}


	private void indexFile(StreamReader fileReader)
	{
        bool invalidConfigFile = false;
		string line;
		string line2;
		indexMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
		configs = new List<Config>();
		while ((line = fileReader.ReadLine()) != null && (line2 = fileReader.ReadLine()) != null)
		{
			try
			{
				Config newConfig = new Config(line, line2);
				configs.Add(newConfig);
				indexMap[line.Trim()] = configs.Count - 1;
			}
			catch (InvalidFileTypeException e) 
			{
				invalidConfigFile = true;
				Debug.Log("Invalid Config:" + line + e.ToString());
				continue;
			}
			catch ( InvalidParameterException e) 
			{
				invalidConfigFile = true;
				Debug.Log("Invalid Config:" + line + e.ToString());
				continue;
			}
		}
		if (invalidConfigFile) updateFile();
	}


	private void createFile()
	{
		try
		{
			File.Create(configFilePath).Dispose();
		}
		catch (IOException e)
		{
			Debug.Log(e.ToString());
		}
	}

	private void updateFile()
	{
		StringBuilder fileString = new StringBuilder();

		foreach (Config config in configs)
		{
			fileString.AppendLine(config.ToString());
		}

		File.WriteAllText(configFilePath, fileString.ToString());
    }
	#endregion

	#region  changing config file
	public void changeConfig(string fileName, string[] headerNames) 
	{

		string configString = string.Join(",", headerNames);

		Config configChange = null;
		try { configChange = new Config(fileName, configString); }
		catch (InvalidFileTypeException e)
		{
			Debug.Log("Invalid Config:" + fileName + e.ToString());
			return;
		}
		catch (InvalidParameterException e)
		{
			Debug.Log("Invalid Config:" + configString + e.ToString());
			return;
		}


		using (StreamWriter fileWriter = new StreamWriter(configFilePath, false))
		{
            if (!indexMap.ContainsKey(fileName))
            {
                fileWriter.Write(addConfig(configChange));
            }
            else
            {
                fileWriter.Write(replaceConfig(fileName, configChange));
            }
        }
	}

    public void changeConfig(string fileName, DebrisParameter[] headerNames)
	{
		string[] headers = new string[headerNames.Length];
        for (int i = 0; i < headerNames.Length; i++)
        {
			headers[i] = headerNames[i].ToString().Trim().ToUpper();
        }
		changeConfig(fileName, headers);
    }


    private string addConfig(Config config)
	{
		string stringBeforeConfig = getstringBeforeConfig(configs.Count);
		configs.Add(config);
        indexMap[config.configFilePath] = configs.Count - 1;
        return stringBeforeConfig + config.ToString();
	}

	private string replaceConfig(string fileName, Config config)
	{
		string stringBeforeConfig = getstringBeforeConfig(indexMap[fileName]);
		string stringAfterConfig = getstringAfterConfig(indexMap[fileName]);

		configs[indexMap[fileName]] = config;

		return stringBeforeConfig + (config.ToString() + Environment.NewLine) + stringAfterConfig;
	}

	public void removeConfig(string fileName)
	{
		if (!indexMap.ContainsKey(fileName)) return;
		string stringBeforeLine = getstringBeforeConfig(indexMap[fileName]);
		string stringAfterLine = getstringAfterConfig(indexMap[fileName]);
		string configString = (stringBeforeLine + stringAfterLine);

        int indexRemoved = indexMap[fileName];
        configs.RemoveAt(indexMap[fileName]);
        indexMap.Remove(fileName);
        for (int i = indexRemoved; i < configs.Count; i++)
        {
            indexMap[configs[i].configFilePath] = i;
        }

        using (StreamWriter fileWriter = new StreamWriter(configFilePath, false))
		{
			fileWriter.Write(configString);
        }
	}

	private string getstringBeforeConfig(int configNumber)
	{
		StringBuilder configString = new StringBuilder();
		for (int i = 0; i < configNumber; i++)
		{
			configString.AppendLine(configs[i].ToString());
		}
		return configString.ToString();
	}

	private string getstringAfterConfig(int configNumber)
	{
        StringBuilder configString = new StringBuilder();
        for (int i = (configNumber + 1); i < configs.Count; i++)
		{
            configString.AppendLine(configs[i].ToString());
        }
		return configString.ToString();
	}

	#endregion

	public string[] getConfigFilePaths() 
	{
		string[] filePaths = new string[configs.Count];

        for (int i = 0; i < configs.Count; i++)
        {
            filePaths[i] = configs[i].configFilePath;
        }

		return filePaths;
    }


	public DebrisParameter[] getConfigParameters(string filePath)
	{
		string[] stringParameters = configs[indexMap[filePath]].configuration.Split(',', StringSplitOptions.RemoveEmptyEntries);

		DebrisParameter[] parameters = new DebrisParameter[stringParameters.Length];

		for (int i = 0; i < stringParameters.Length; i++)
		{
			if (!Enum.TryParse(stringParameters[i].Trim().ToUpper(), false, out parameters[i])) 
			{
                parameters[i] = DebrisParameter.NULL;
            }
		}
		return parameters;
	}
}