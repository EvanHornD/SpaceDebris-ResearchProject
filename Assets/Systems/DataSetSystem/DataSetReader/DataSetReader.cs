using System.IO;
using System;
using UnityEngine;

public class DataSetReader
{

	public static bool isCSV(string fileLocation)
	{
        if (!File.Exists(fileLocation))
        {
            Console.WriteLine("DataSet File not found at: " + fileLocation);
			throw new FileNotFoundException();
        }

        using (StreamReader dataSetReader = new StreamReader(fileLocation))
        {
            string line = dataSetReader.ReadLine();
            int numCommas = line.Split(",").Length - 1;

            if (numCommas == 0)
            {
                Debug.Log("Not parseable header without any commas");
                return false;
            }

            while ((line = dataSetReader.ReadLine()) != null)
            {
                if ((line.Split(",").Length - 1) != numCommas) return false;
            }
            return true;
        }
	}

	public static string[] parseHeader(string fileLocation) // probably useless
	{
		using (StreamReader dataSetReader = new StreamReader(fileLocation))
		{
			string[] headers = dataSetReader.ReadLine().Split(",");
			string[] parameters = new string[headers.Length];

			for (int i = 0; i < headers.Length; i++)
			{
                try
                {
                    DebrisParameter parameter;
                    Enum.TryParse(headers[i].Trim(), true, out parameter);
                    parameters[i] = parameter.ToString();
				}
				catch (Exception)
                {
					parameters[i] = "NULL";
				}
			}
			return parameters;
		}
	}

    public static string[] getHeader(string fileLocation)
    {
        using (StreamReader dataSetReader = new StreamReader(fileLocation))
        {
            string[] headers = dataSetReader.ReadLine().Split(",");
            return headers;
        }
    }

    public static DebrisEntry[] parseDataEntries(string fileLocation, int numEntries, DebrisParameter[] parameters)
	{
		DebrisEntry[] debris = new DebrisEntry[numEntries];
		using (StreamReader dataSetReader = new StreamReader(fileLocation))
		{
			string line = dataSetReader.ReadLine();
			int currentLine = -1;
			while ((line = dataSetReader.ReadLine()) != null && (currentLine += 1) < numEntries)
			{
				string[] entry = line.Split(",");
				if (entry.Length != parameters.Length)
				{
					Debug.Log("Error reading the data set, " + line + " doesnt have the same number of parameters as the config");
					return null;
				}
				debris[currentLine] = createDebrisEntry(entry, parameters);
			}
			return debris;
		}
	}

	private static DebrisEntry createDebrisEntry(string[] entry, DebrisParameter[] parameters)
	{
		DebrisEntry debrisEntry = new DebrisEntry();
		for (int i = 0; i < entry.Length; i++)
		{
			if (parameters[i] == DebrisParameter.NULL) continue;

			switch (parameters[i])
			{
				case DebrisParameter.NAME: debrisEntry.NAME = entry[i]; break;
				case DebrisParameter.CATALOGNUMBER: debrisEntry.CATALOGNUMBER = entry[i]; break;
				case DebrisParameter.OBJECTTYPE: debrisEntry.OBJECTTYPE = entry[i]; break;
				case DebrisParameter.TLELINE0: debrisEntry.TLELINE0 = entry[i]; break;
				case DebrisParameter.TLELINE1: debrisEntry.TLELINE1 = entry[i]; break;
				case DebrisParameter.TLELINE2: debrisEntry.TLELINE2 = entry[i]; break;
				case DebrisParameter.ORBITALCLASSIFICATION: debrisEntry.ORBITALCLASSIFICATION = entry[i]; break;
				case DebrisParameter.MEANANOMOLY: debrisEntry.MEANANOMOLY = entry[i]; break;
				case DebrisParameter.EPOCHTIME: debrisEntry.EPOCHTIME = entry[i]; break;
				case DebrisParameter.ARGUMENTOFPERIGEE: debrisEntry.ARGUMENTOFPERIGEE = entry[i]; break;
				case DebrisParameter.INCLINATION: debrisEntry.INCLINATION = entry[i]; break;
				case DebrisParameter.RAAN: debrisEntry.RAAN = entry[i]; break;
				case DebrisParameter.PERIOD: debrisEntry.PERIOD = entry[i]; break;
				case DebrisParameter.SEMIMAJORAXIS: debrisEntry.SEMIMAJORAXIS = entry[i]; break;
				case DebrisParameter.SEMIMINORAXIS: debrisEntry.SEMIMINORAXIS = entry[i]; break;
				case DebrisParameter.PERIGEE: debrisEntry.PERIGEE = entry[i]; break;
				case DebrisParameter.APOGEE: debrisEntry.APOGEE = entry[i]; break;
				case DebrisParameter.ECCENTRICITY: debrisEntry.ECCENTRICITY = entry[i]; break;
				case DebrisParameter.MEANMOTION: debrisEntry.MEANMOTION = entry[i]; break;
				case DebrisParameter.REVOLUTIONNUMBER: debrisEntry.REVOLUTIONNUMBER = entry[i]; break;
				case DebrisParameter.DRAGCOEFFICIENT: debrisEntry.DRAGCOEFFICIENT = entry[i]; break;
				case DebrisParameter.AREA: debrisEntry.AREA = entry[i]; break;
				case DebrisParameter.MASS: debrisEntry.MASS = entry[i]; break;
				default: break;
			}
		}
		return debrisEntry;
	}
}
