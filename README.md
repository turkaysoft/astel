# Astel - Password Management Software

[![GitHub downloads](https://img.shields.io/github/downloads/turkaysoft/astel/total?style=flat&color=1a893c&label=Downloads)](https://github.com/turkaysoft/astel/releases)
[![GitHub stars](https://img.shields.io/github/stars/turkaysoft/astel?style=flat&color=0062cc&label=Stars)](https://github.com/turkaysoft/astel/stargazers)
[![GitHub release](https://img.shields.io/github/v/release/turkaysoft/astel?style=flat&color=5a32a3&label=Latest%20Release)](https://github.com/turkaysoft/astel/releases/latest)
[![Platform](https://img.shields.io/badge/platform-Windows-b31d28?style=flat&label=Platform)](https://github.com/turkaysoft/astel)

**Astel** is a secure and modern **password management software** developed by **Eray Türkay**. Designed with a strong focus on privacy and security, Astel keeps your data entirely on your computer and protects it with a secure, encrypted vault. With modern password protection, automatic locking, secure backups, and flexible data management, Astel provides a private and reliable way to manage your digital credentials.

---

### Donate
You can support this project by making a donation to help ensure its sustainability and the development of new features.

[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Donate-0a6628?style=flat&logo=buy-me-a-coffee&logoColor=white)](https://buymeacoffee.com/turkaysoft)

---

## Key Features

* **Privacy First:** Your data stays on your machine; no information is transferred to external servers.
* **Pure Performance:** Developed exclusively in **C# and .NET Framework** with no external libraries or dependencies.
* **Portable:** No installation required. Just download it, extract all files from the ZIP, select the appropriate architecture, and run it.
* **Secure Vault:** Uses the new **Vault Format v2** with PBKDF2-HMAC-SHA-512 and 210,000 iterations for stronger protection against brute-force attacks.
* **Strong Passwords:** Supports master passwords from **12 to 128 characters** and includes a modern, cryptographically secure password generator with lengths from 12 to 48 characters.
* **Automatic Lock:** Automatically locks the vault after 5 minutes of inactivity and clears sensitive data from memory.
* **Secure Data Management:** Provides safe vault operations, automatic backups, memory protection, and single-instance execution to help prevent data loss and conflicts.
* **Secure Clipboard:** Sensitive data copied by the application is automatically cleared from the clipboard when the application closes.
* **Flexible Data Transfer:** Supports encrypted `*.astel` and standard `*.csv` import and export with additional security protections.
* **Modern UI:** Clean, intuitive interface compatible with Windows 11 design language, featuring Light, Dark, and System themes.
* **Multilingual:** It supports 16 different languages, primarily English. You can access the supported languages here: [Supported Languages](https://github.com/turkaysoft/astel/discussions/3)
* **Built-in Update Mechanism:** It features a built-in smart update mechanism developed specifically by **Türkaysoft**.

---

## Interface Preview

<img width="1010" height="633" alt="Astel UI" src="https://github.com/user-attachments/assets/43132ac2-74a5-4ed0-bcfa-dd621345dbf6" />

## Password Generator

<img width="586" height="557" alt="Astel Password Generator" src="https://github.com/user-attachments/assets/9bddd3bc-7233-407f-8782-90c1ac827209" />

---

## Getting Started

1.  Navigate to the **[Releases](https://github.com/turkaysoft/astel/releases/latest)** page.
2.  Download the latest ZIP file.
3.  **Extract all files from the ZIP** (Important: Application requires all folder contents to run correctly).
4.  Launch the executable corresponding to your architecture:
    * `Astel_x64.exe`: For standard 64-bit Intel/AMD systems.
    * `Astel_arm64.exe`: For ARM-based devices like Surface Pro.

---

## Translation Support

* **Translation Support:** Community-driven localization via the official [Translation Guide](https://github.com/turkaysoft/astel/discussions/3).

---

## System Requirements

| Feature | Minimum Requirements | Recommended Requirements |
| :--- | :--- | :--- |
| **OS** | Windows 10 22H2 x64 | Windows 11 25H2 x64 |
| **CPU** | x64 or ARM64 | x64 or ARM64 |
| **RAM** | 50 MB Free RAM | 75 MB Free RAM |
| **.NET** | .NET Framework 4.8.1 | .NET Framework 4.8.1 |

---

## Shortcut Keys

| Shortcut | Action |
|--|--|
| `F1` | Light Theme |
| `F2` | Dark Theme |
| `F3` | System Theme |
| `F4` | Starting With: Windowed |
| `F5` | Starting With: Full Screen |
| `F6` | Safety Warnings: On |
| `F7` | Safety Warnings: Off |
| `F8` | Password Mask: On |
| `F9` | Password Mask: Off |
| `F10` | Change Password |
| `F11` | Check Updates |
| `F12` | About |
| `CTRL + P` | Password Generator |
| `CTRL + Alt + D` | Donate Page |
| `CTRL + Shift 1` | Export Astel File |
| `CTRL + Shift 2` | Export CSV File |
| `CTRL + Shift 3` | Import Astel File |
| `CTRL + Shift 4` | Import CSV File |
| `CTRL + Shift 5` | Backup On |
| `CTRL + Shift 6` | Backup Off |
| `CTRL + Shift 7` | Open Backup Folder |
| `ESC` | Clear Selection |

---

## Security

* **Zero Data Export Policy:** Your privacy is our priority; no data leaves your machine.
* **No Dependencies:** Developed entirely from scratch using its own source code, there are no risks from security vulnerabilities in third-party libraries.
* **Open Source:** All source code for the program is open and can be reviewed by anyone.

---

## License

This software is offered free of charge as part of the **Türkaysoft solutions package** and is protected under the [**MIT License**](https://github.com/turkaysoft/astel?tab=MIT-1-ov-file).
