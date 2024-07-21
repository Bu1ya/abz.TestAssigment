
# CurrencyRateService

## Overview
A Windows service application that fetches currency rates from the National Bank of Ukraine (NBU) API and saves them in a specified format.

## Installation
1. Download `CurrencyRateService.exe` from the [GitHub release page](https://github.com/Bu1ya/abz.TestAssigment/releases/tag/TestAssigment).
2. Place it in any target directory where you want to store it.
3. Open CMD in Administrator mode, navigate to the directory with `CurrencyRateService.exe`, and run `CurrencyRateService.exe -install`.

After these steps, the service should be installed.

## Configuration
A configuration file named `AppSettings.xml` is created in your target directory. You can modify the following settings for the service:

- `FetchInterval`: The interval in seconds for fetching the currency rates.
- `DataFormat`: The format to save the data (json, csv, xml).
- `OutputFilePath`: The path to save the fetched data.

**Important:** When specifying the file path, do not include the file extension.

**Example:**

```xml
<Config>
    <FetchInterval>3600</FetchInterval>
    <OutputFormat>xml</OutputFormat>
    <OutputFilePath>C:\path\to\your\Result</OutputFilePath>
</Config>
```

Incorrect:
```xml
<OutputFilePath>C:\path\to\your\Result.xml</OutputFilePath>
```

## Usage
Start the service from the Services management console. You can find it in the Windows menu or press `Win+R` and type `services.msc`.

You can dynamically change the configuration with the following commands:

1. Open CMD in Administrator mode and navigate to the directory with `CurrencyRateService.exe`.
2. Run `CurrencyRateService.exe -fmt <format>` to change the format of the result file. `<format>` can be `xml`, `csv`, or `json`.
3. Run `CurrencyRateService.exe -out <Path\to\your\file>` to change the path where the result file will be saved. **Do not add an extension to the file.**

**Incorrect:**
```
<Path\to\your\file.xml>
```

4. Run `CurrencyRateService.exe -out <Path\to\your\file> -fmt <format>` to change both the path where the result file will be saved and the file format.

## Uninstallation
Open CMD in Administrator mode, navigate to the directory with `CurrencyRateService.exe`, and run `CurrencyRateService.exe -uninstall`.

Make sure that you stop the service before uninstalling it.

## Troubleshooting
Check log files in the `Logs` directory for errors and service activity. In the `Logs\History` directory, you will find logs from previous uses.

If you can't find the service in the Services management console after installation, ensure that you ran CMD in Administrator mode.

If that doesn't work, navigate to the CurrencyRateService.exe file and open the properties. Check if there is a message at the bottom of the General tab: `Security: This file came from another computer and might be blocked to help protect this computer.` If so, click the unlock button next to the message

# Documentation
You can also check out the high-level code overview [document](https://github.com/Bu1ya/abz.TestAssigment/blob/main/Code_Structure_and_Modules_Overview.md)

And [document](https://github.com/Bu1ya/abz.TestAssigment/blob/main/External_APIs_and_Libraries_Documentation.md) which lists the packages I used to develop the service
