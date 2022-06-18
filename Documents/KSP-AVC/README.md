# KSP Add-on Version Checker

### Developer

CYBUTEK

### KSP-AVC Ready

KSP Add-on Version Checker is a standardised system for versioning mods.
You can get more information on the [forum
thread](http://forum.kerbalspaceprogram.com/threads/79745)

### Description

On starting KSP this plugin will search your game directory for
`.version` files. It will then proceed to check each one for version
related issues. If any issues are found, an issue monitor will be
displayed notifying you of what they are. There are two main types of
version issues, with the first being related to the add-on\'s version
and the second being its compatibility with your version of KSP. Some
add-ons will also support a download option in their version check. If
you require an update of an add-on and it has a download location set,
you will be given a button which will open up your default browser and
take you there. This could link directly to the .zip file or to a page
with details on how to update. Note that you will need to close down
KSP, install the updates and then restart KSP for them to work.

### Installation

* Copy the `KSP-AVC` folder into the `GameData` folder located within your Kerbal Space Program installation directory.

### Version File Breakdown

* **NAME** - Required
	+ The display name for the add-on.
* **URL** - Optional
	+ Location of a remote version file for update checking.
* **DOWNLOAD** - Optional
	+ Web address where the latest version can be downloaded.
    + *This is only used from the remote version file.*
* **CHANGE_LOG** - Optional
	+ The complete or incremental change log for the add-on.
    + *This is only used from the remote version file.*
* **CHANGE_LOG_URL** - Optional
    + Populates the CHANGE_LOG field using the file at this url.
    + *This is only used from the remote version file.*
* **GITHUB** - Optional
	+ Allows KSP-AVC to do release checks with GitHub including setting a download location if one is not specified.
	+ If the latest release version is not equal to the version in the file, an update notification will not appear.
	+ *This is only used from the remote version file.*
		- **USERNAME** - Required
			- Your GitHub username.
		- **REPOSITORY** - Required
			- The name of the source repository.
		- **ALLOW_PRE_RELEASE** - Optional
			- Include pre-releases in the latest release search.
			- *The default value is false.*
* **VERSION** - Required
	+ The version of the add-on.
* **KSP_VERSION** - Optional, Required for MIN/MAX
	+ Version of KSP that the add-on was made to support.
* **KSP_VERSION_MIN** - Optional
	+ Minimum version of KSP that the add-on supports.\
	+ *Requires KSP_VERSION field to work.*
* **KSP_VERSION_MAX** - Optional
	+ Maximum version of KSP that the add-on supports.
    + *Requires KSP_VERSION field to work.*

For simple management of your version files you can use the KSP-AVC
Online website at: [ksp-avc.cybutek.net](http://ksp-avc.cybutek.net)

### Version File Example

```
{
    "NAME":"KSP-AVC",
    "URL":"http://ksp-avc.cybutek.net/version.php?id=2",
    "DOWNLOAD":"http://kerbal.curseforge.com/ksp-mods/220462-ksp-avc-add-on-version-checker",
    "GITHUB":
    {
        "USERNAME":"YourGitHubUserName",
        "REPOSITORY":"YourGitHubRepository",
        "ALLOW_PRE_RELEASE":false,
    },
    "VERSION":
    {
        "MAJOR":1,
        "MINOR":1,
        "PATCH":0,
        "BUILD":0
    },
    "KSP_VERSION":
    {
        "MAJOR":0,
        "MINOR":24,
        "PATCH":2
    },
    "KSP_VERSION_MIN":
    {
        "MAJOR":0,
        "MINOR":24,
        "PATCH":0
    },
    "KSP_VERSION_MAX":
    {
        "MAJOR":0,
        "MINOR":24,
        "PATCH":2
    }
}
```

### Software License

Licensed under the [GNU General Public License v3](LICENSE.txt).

README design by [**CYBUTEK**](http://ksp.cybutek.net)
(GPLv3)
