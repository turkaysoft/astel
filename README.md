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
* **DPAPI Session Encryption:** Session information is encrypted using Windows Data Protection API (DPAPI), ensuring data can only be decrypted on the same user account and the same machine.
* **Strong Brute-Force Protection:** Master passwords and sensitive data are secured using PBKDF2-HMAC-SHA512 with 210,000 iterations and a unique salt, making brute-force attacks extremely difficult.
* **Cryptographically Secure Password Generator:** Uses `RandomNumberGenerator`, implements Rejection Sampling to eliminate modulo bias, and applies Fisher-Yates Shuffle for secure character randomization. Each generated password guarantees at least one uppercase letter, one lowercase letter, one digit, and one symbol, with a random length between 10-18 characters.
* **Auto-Clipboard Clear:** Copied passwords are automatically removed from the clipboard after 30 seconds, preventing accidental exposure through paste operations.
* **Masked Password Display:** Password data is shown as masked characters (●) in tables, protecting against shoulder surfing and onlookers.
* **Memory Security:** Sensitive data references are actively cleaned from memory using secure memory clearing methods, protecting against RAM analysis attacks.
* **Pure Performance:** Developed using only C# and .NET Framework with **zero external libraries** or dependencies.
* **Modern UI:** Clean interface with Light, Dark, and System theme support for a seamless Windows experience.
* **Multilingual:** It supports 15 different languages, primarily English. You can access the supported languages here: [Supported Languages](https://github.com/turkaysoft/astel/discussions/3)
* **Portable:** No installation required. Simply extract and run to access your vault.
* **Automatic Backup:** Encrypted backups are created at specified intervals to prevent any data loss.
* **Built-in Update Mechanism:** It features a built-in smart update mechanism developed specifically by **Türkaysoft**.

---

## Interface Preview

<img width="1010" height="633" alt="Astel UI" src="https://github.com/user-attachments/assets/42ca5bb5-e3b3-4608-9bdc-4823c62e7b74" />

## Password Generator

<img width="586" height="533" alt="Astel Password Generator" src="https://github.com/user-attachments/assets/54ecb11a-fffd-40c2-ad66-337c7dc5623d" />

---

## Advanced Capabilities

Astel goes beyond simple storage, offering tools to manage your digital identity securely:

* **Advanced Password Generator:** Create complex, secure passwords tailored to your specific requirements.
* **Flexible Data Transfer:** Easily export or import your vault data in both encrypted `*.astel` and standard `*.csv` formats.
* **Multi-Architecture Support:** Native compatibility for x64, x86, and ARM64 processor architectures.

---

## Translation Support

* **Translation Support:** Community-driven localization via the official [Translation Guide](https://github.com/turkaysoft/astel/discussions/3).

---

## System Requirements

| Feature | Minimum Requirements | Recommended Requirements |
| :--- | :--- | :--- |
| **OS** | Windows 10 20H2 x64 | Windows 10 22H2 x64 |
| **CPU** | x64 or ARM64 | x64 or ARM64 |
| **RAM** | 50 MB Free RAM | 75 MB Free RAM |
| **.NET** | .NET Framework 4.8.1 | .NET Framework 4.8.1 |

---

## Getting Started

1.  Navigate to the **[Releases](https://github.com/turkaysoft/astel/releases/latest)** page.
2.  Download the latest ZIP file.
3.  **Extract all files from the ZIP** (Important: Application requires all folder contents to run correctly).
4.  Launch the executable corresponding to your architecture:
    * `Astel_x64.exe`: For standard 64-bit Intel/AMD systems.
    * `Astel_arm64.exe`: For ARM-based devices like Surface Pro.

---

## Security

* **Zero Data Export Policy:** Your privacy is our priority; no data leaves your machine.
* **No Dependencies:** Developed entirely from scratch using its own source code, there are no risks from security vulnerabilities in third-party libraries.
* **Open Source:** All source code for the program is open and can be reviewed by anyone.

---

## License

This software is offered free of charge as part of the **Türkaysoft solutions package** and is protected under the [**MIT License**](https://github.com/turkaysoft/astel?tab=MIT-1-ov-file).
