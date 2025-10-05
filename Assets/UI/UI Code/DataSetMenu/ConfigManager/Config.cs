using System;
using System.IO;

public class Config
{
    public string configFilePath;
    public string configuration;

    public Config(string filePath, string configuration) {
        this.configFilePath = filePath;
        this.configuration = configuration;

        string extention = Path.GetExtension(filePath);
        if(!(string.Equals(extention, ".txt") || string.Equals(extention, ".csv"))){
            throw new InvalidFileTypeException("DataSets can only be of FileType: .txt, and .csv The filetype:" + extention + " is an invalid filetype");
        }

        string[] headers = configuration.Split(",");
        for (int i = 0; i < headers.Length; i++)
        {
            if (!Enum.TryParse<DebrisParameter>(headers[i], false, out _)) 
            {
                throw new InvalidParameterException("The Parameter " + headers[i] + " is not a valid Debris Parameter");
            }
        }
    }

    public override string ToString()
    {
        return this.configFilePath + "\n" + this.configuration;
    }
}