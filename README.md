# YKWriter 🔑💾

**YKWriter** is a lightweight Windows Forms utility designed to automate the preparation of USB recovery media using the YellowKey exploit framework (**CVE-2026-45585**).

It simplifies the staging process for data recovery technicians by handling the partition formatting and directory alignment required to trigger the WinRE BitLocker bypass chain. Instead of manually managing hidden system folders, the application formats a selected USB drive to exFAT and automatically deploys the necessary exploit files.

---

## ⚡ Features

* **One-Click Format & Stage:** Formats the target USB drive to exFAT and writes the exploit directory payload in a single operation.
* **Exploit Path Alignment:** Automatically structures the payload under the precise physical path required by the vulnerability (`\System Volume Information\FsTx\`).
* **Safe Drive Selection:** Filters out system-critical drives to prevent accidental formatting of internal OS partitions.

---

## 🛠️ Requirements

* **Operating System:** Windows 10 / 11 (Requires Administrative privileges to perform low-level drive formatting).
* **Framework:** .NET 8.0
* **Hardware:** A standard USB thumb drive (all data will be wiped).

---

## 📖 How to Use

1. **Launch the Application:** Run `YKWriter.exe` as an **Administrator** (right-click -> Run as Administrator) so the tool can access the disk formatting APIs.
2. **Insert Target USB:** Plug in the thumb drive you intend to use as your recovery key.
3. **Select Your Drive:** Choose the correct drive letter from the dropdown menu.
4. **Deploy:** Click **"Run"**. The utility will:
* Wipe and format the target drive to **exFAT**.
* Create the required `System Volume Information\FsTx` directory structure.
* Write the payload components to the folder.


5. **Eject:** Once the progress bar fills and the "Success" prompt appears, safely eject the USB.

---

## 🚀 Execution Workflow (Target Machine)

Once the USB is staged via YKWriter:

1. Insert the prepared USB drive into the unbootable, BitLocker-encrypted Windows 11 / Server machine.
2. Boot or force a restart into the **Windows Recovery Environment (WinRE)**.
3. Hold down the exploit's key combination (e.g., `CTRL` key during the trigger phase) to bypass the standard recovery interface.
4. The system will drop into an unrestricted command prompt with the primary volume fully accessible for file salvage and data extraction.

---

## ⚠️ Safety Disclaimers

* **Data Loss Warning:** Running this tool completely erases the selected USB drive. Double-check your selected drive letter before clicking format.
* **Authorized Use:** This utility is provided strictly for legitimate data recovery, disaster recovery testing, and administrative system repair. Ensure you have explicit authorization on the target hardware before utilizing the staged exploit media.

---

## 🤝 Credits & Acknowledgments

* **Exploit Discovery & Release:** A huge thanks to **Nightmare Eclipse** ([GitLab Profile](https://gitlab.com/nightmare-eclipse)) for identifying and releasing the CVE-2026-45585 vulnerability.
* **Core Tooling:** The execution workflow and recovery bypass methods utilized by this project are based on the [YellowKey repository](https://gitlab.com/nightmare-eclipse/YellowKey).

--- 

📄 License

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.