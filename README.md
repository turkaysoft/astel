# Astel - Advanced Password Management Software

[![GitHub downloads](https://img.shields.io/github/downloads/turkaysoft/astel/total?style=flat&color=1a893c&label=Downloads)](https://github.com/turkaysoft/astel/releases)
[![GitHub stars](https://img.shields.io/github/stars/turkaysoft/astel?style=flat&color=0062cc&label=Stars)](https://github.com/turkaysoft/astel/stargazers)
[![GitHub release](https://img.shields.io/github/v/release/turkaysoft/astel?style=flat&color=5a32a3&label=Latest%20Release)](https://github.com/turkaysoft/astel/releases/latest)
[![Platform](https://img.shields.io/badge/platform-Windows-b31d28?style=flat&label=Platform)](https://github.com/turkaysoft/astel)

**Astel** is a secure and powerful **password management software** developed by **Eray Türkay**. Built with a focus on maximum security, it ensures that none of your personal information ever leaves your computer. Every piece of data is stored locally and encrypted, providing a private vault for your digital life.

---

### Donate
You can support this project by making a donation to help ensure its sustainability and the development of new features.

[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Donate-0a6628?style=flat&logo=buy-me-a-coffee&logoColor=white)](https://buymeacoffee.com/turkaysoft)

---

## Key Features

* **Privacy First:** Your data stays on your machine; no information is transferred to external servers.
* **Pure Performance:** Developed exclusively in **C# and .NET Framework** with no external libraries or dependencies.
* **Portable:** No installation required. Just download it, extract all files from the ZIP, select the appropriate architecture, and run it.
* **DPAPI Session Encryption:** Session information is encrypted using Windows Data Protection API (DPAPI), ensuring data can only be decrypted on the same user account and the same machine.
* **Strong Brute-Force Protection:** Master passwords and sensitive data are secured using PBKDF2-HMAC-SHA256 with 100,000 iterations and a unique salt, while AES key derivation is performed with HKDF, making brute-force attacks extremely difficult.
* **Cryptographically Secure Password Generator:** Uses `RandomNumberGenerator`, implements Rejection Sampling to eliminate modulo bias, and applies Fisher-Yates Shuffle for secure character randomization. Each generated password guarantees at least one uppercase letter, one lowercase letter, one digit, and one symbol, with a random length between 12-16 characters.
* **Auto-Clipboard Clear:** Copied passwords are automatically removed from the clipboard after 30 seconds, preventing accidental exposure through paste operations.
* **Masked Password Display:** Password data is shown as masked characters (●) in tables, protecting against shoulder surfing and onlookers.
* **Memory Security:** Sensitive data references are actively cleaned from memory using secure memory clearing methods, protecting against RAM analysis attacks.
* **Advanced Password Generator:** Create complex, secure passwords tailored to your specific requirements.
* **Flexible Data Transfer:** Easily export or import your vault data in both encrypted `*.astel` and standard `*.csv` formats.
* **Automatic Backup:** Encrypted backups are created at specified intervals to prevent any data loss.
* **Multilingual:** It supports 15 different languages, primarily English. You can access the supported languages here: [Supported Languages](https://github.com/turkaysoft/astel/discussions/3)
* **Modern UI:** Clean, intuitive interface compatible with Windows 11 design language, featuring Light, Dark, and System themes.
* **Built-in Update Mechanism:** It features a built-in smart update mechanism developed specifically by **Türkaysoft**.

---

## Interface Preview

<img width="1010" height="633" alt="Astel UI" src="https://github.com/user-attachments/assets/4e22aeca-fc45-4cb2-a675-8133c8439b97" />

## Password Generator

<img width="586" height="523" alt="Astel Password Generator" src="https://github.com/user-attachments/assets/0275dd5d-6ec3-461c-8e3c-8012bc706a96" />

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
