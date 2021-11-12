# # KSP Add'On Version Checker /L Unleashed :: Change Log

* 2014-0816: 1.1.3 (cybutek) for KSP 0.24.2
	+ Added: GitHub latest release checking.
	+ Added: Version file reading is now case insensitive.
	+ Fixed: Bug in version equality checking. (Now using a custom VersionInfo object).
* 2014-0809: 1.1.2 (cybutek) for KSP 0.24.2
	+ Added: Tooltip when hovering over the download button showing the destination URL.
	+ Fixed: Certain situations would cause the root directory to be incorrect.
	+ Fixed: File not found handling solving the lock whilst on the checking display.
* 2014-0807: 1.1.1 (cybutek) for KSP 0.24.2
	+ Added: Blanket exception handling and logging.
	+ Added: Logger will flush on destruction and GC finaliser.
	+ Added: Check to remove '/tree/' from github urls.
	+ Changed: Remote checking back to WWW because WebRequest in Mono does not support TLS/SSL.
	+ Changed: Version file search is now limited to just the GameData directory.
	+ Fixed: Possible null refs caused by invalid remote urls.
* 2014-0806: 1.1 (cybutek) for KSP 0.24.2
	+ Complete re-write of the core code.
	+ Added: Replaced LitJson with embeded MiniJson for compatibility and to reduce dependancies.
	+ Added: Better utilisation of multi-threading.
	+ Added: Check progress window which will show whilst processing.
	+ Changed: Remote version file fetching now uses WebRequest instead of Unity's archaic WWW.
	+ Fixed: Version formatting bug where it did not recognise build numbers in certain cases.
* 2014-0726: 1.0.4 (cybutek) for KSP 0.24.1
	+ Added: Url check to fix problems caused by non-raw github .version files.
	+ Added: Logging system now also saves into the standard ksp log file.
	+ Changed: Extended logging system now saves with the associated '.dll' file.
* 2014-0725: 1.0.3 (cybutek) for KSP 0.24.1
	+ Updated for KSP version 0.24.2.
	+ Added: Extended logging system that saves to "KSP-AVC.log".
	+ Added: First run checker which will show to indicate a successful install.
	+ Changed: Window is now centred on the screen.
* 2014-0718: 1.0.2 (cybutek) for KSP 0.23.5
	+ Updated for KSP version 0.24.
	+ Fixed a small bug with parsing KSP versions.
* 2014-0513: 1.0.1 (cybutek) for KSP 0.23.5
	+ 1.0.1
		- Added minimum and maximum KSP version support.
		- Updates will only be shown if the remote version is compatible with the
	+ installed version of KSP.
* 2014-0512: 1.0 (cybutek) for KSP 0.23.5
	+ No changelog provided
