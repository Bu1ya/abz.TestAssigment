# Code Structure and Modules Overview

## Introduction
This document provides a high-level overview of the key modules used in the project. It describes the functionality of the most important modules.

## Key Modules

### 1. Program.cs
The entry point of the program. This module starts the service, or if the service is already running, it can be called from the command line to change configuration settings.

### 2. CurrencyRateService.cs
A service base that initiates a repeating algorithm for fetching currency rates.

### 3. CurrencyRateFetcher.cs
Handles fetching data using the NBU API. It performs HTTP requests to retrieve currency rates and then provides it to the file generator.

### 4. ResultGenerators\IResultFileGenerator.cs
An abstract class for three file generators: JSON, CSV, and XML.

### 5. ResultGenerators\JSONFileGenerator.cs
### 6. ResultGenerators\CSVFileGenerator.cs
### 7. ResultGenerators\XMLFileGenerator.cs
These classes generate result documents with the provided currency rates.

### 8. Models\CurrencyRate.cs
A class that represents a currency rate entity.

### 9. Config.cs
A class that represents the configuration file entity and provides methods to work with it. 

### 10. Logger.cs
A singleton class that handles logging during service operation and stores logs from previous sessions.
