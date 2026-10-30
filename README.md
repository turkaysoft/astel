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

<img width="1010" height="633" alt="Astel UI" src="https://github.com/user-attachments/assets/75655905-afa8-456b-8b5a-6e29895b1f17" />


## Password Generator

<img width="586" height="557" alt="Astel Password Generator" src="https://github.com/user-attachments/assets/6bd323bd-0118-48d3-8f6d-50ef4d920ff1" />

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

<details>
<summary><b>📝 Opis v Slovenščini</b></summary>

# Astel - Programska oprema za upravljanje gesel

[![GitHub downloads](https://img.shields.io/github/downloads/turkaysoft/astel/total?style=flat&color=1a893c&label=Downloads)](https://github.com/turkaysoft/astel/releases)
[![GitHub stars](https://img.shields.io/github/stars/turkaysoft/astel?style=flat&color=0062cc&label=Stars)](https://github.com/turkaysoft/astel/stargazers)
[![GitHub release](https://img.shields.io/github/v/release/turkaysoft/astel?style=flat&color=5a32a3&label=Latest%20Release)](https://github.com/turkaysoft/astel/releases/latest)
[![Platform](https://img.shields.io/badge/platform-Windows-b31d28?style=flat&label=Platform)](https://github.com/turkaysoft/astel)

**Astel** je varna in sodobna **programska oprema za upravljanje gesel**, ki jo je razvil **Eray Türkay**. Astel je zasnovan s poudarkom na zasebnosti in varnosti; podatke hrani izključno v vašem računalniku in jih varuje v varnem, šifriranem trezorju. S sodobno zaščito gesel, samodejnim zaklepanjem, varnimi varnostnimi kopijami in prilagodljivim upravljanjem podatkov Astel zagotavlja zaseben ter zanesljiv način za upravljanje vaših digitalnih poverilnic.

---

### Darujte
Ta projekt lahko podprete z donacijo, s čimer pripomorete k zagotavljanju njegove trajnosti in razvoju novih funkcij.

[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Donate-0a6628?style=flat&logo=buy-me-a-coffee&logoColor=white)](https://buymeacoffee.com/turkaysoft)

---

## Ključne značilnosti

* **Zasebnost na prvem mestu:** Vaši podatki ostanejo na vašem računalniku; nobene informacije se ne prenašajo na zunanje strežnike.
* **Čista zmogljivost:** Razvito izključno v **C# in .NET Framework** brez zunanjih knjižnic ali odvisnosti.
* **Prenosljivo:** Namestitev ni potrebna. Preprosto ga prenesite, izvlecite vse datoteke iz ZIP datoteke, izberite ustrezno arhitekturo in ga zaženite.
* **Varni trezor:** Uporablja novo obliko zapisa trezorja **Vault Format v2** s PBKDF2-HMAC-SHA-512 in 210.000 iteracijami za močnejšo zaščito pred napadi z grobo silo.
* **Močna gesla:** Podpira glavna gesla od **12 do 128 znakov** in vključuje sodoben, kriptografsko varen generator gesel z dolžino od 12 do 48 znakov.
* **Samodejno zaklepanje:** Samodejno zaklene trezor po 5 minutah neaktivnosti in izbriše občutljive podatke iz pomnilnika.
* **Varno upravljanje podatkov:** Zagotavlja varno delovanje trezorja, samodejno varnostno kopiranje, zaščito pomnilnika in izvajanje v enem samem primeru, kar pomaga preprečiti izgubo podatkov in konflikte.
* **Varno odložišče:** Občutljivi podatki, ki jih aplikacija kopira, se samodejno izbrišejo iz odložišča, ko se aplikacija zapre.
* **Prilagodljiv prenos podatkov:** Podpira šifriran uvoz in izvoz datotek `*.astel` in standardnih datotek `*.csv` z dodatnimi varnostnimi zaščitami.
* **Sodoben uporabniški vmesnik:** Čist, intuitiven vmesnik, združljiv z oblikovalskim jezikom sistema Windows 11, s svetlimi, temnimi in sistemskimi temami.
* **Večjezičnost:** Podpira 16 različnih jezikov, predvsem angleščino. Do podprtih jezikov lahko dostopate tukaj: [Supported Languages](https://github.com/turkaysoft/astel/discussions/3)
* **Vgrajen mehanizem posodabljanja:** Ima vgrajen pametni mehanizem posodabljanja, ki ga je posebej razvil **Türkaysoft**.

---

## Predogled vmesnika

<img width="1010" height="633" alt="Astel UI" src="https://github.com/user-attachments/assets/75655905-afa8-456b-8b5a-6e29895b1f17" />


## Generator gesel

<img width="586" height="557" alt="Astel Password Generator" src="https://github.com/user-attachments/assets/6bd323bd-0118-48d3-8f6d-50ef4d920ff1" />

---

## Začetek

1.  Pomaknite se na **[Releases](https://github.com/turkaysoft/astel/releases/latest)** stran.
2.  Prenesite najnovejšo ZIP datoteko.
3.  **Razširite vse datoteke iz datoteke ZIP** (Pomembno: za pravilno delovanje aplikacije je potrebna celotna vsebina mape).
4.  Zaženite izvedljivo datoteko, ki ustreza vaši arhitekturi:
    * `Astel_x64.exe`: Za standardne 64-bitne Intel/AMD sisteme.
    * `Astel_arm64.exe`: Za naprave na osnovi ARM, kot je Surface Pro.

---

## Podpora pri prevajanju

* **Podpora pri prevajanju:** Lokalizacija, ki jo vodi skupnost, prek uradnega [Vodiča za prevajanje](https://github.com/turkaysoft/astel/discussions/3).

---

## Sistemske zahteve

| Značilnosti | Minimalne zahteve | Priporočene zahteve |
| :--- | :--- | :--- |
| **OS** | Windows 10 22H2 x64 | Windows 11 25H2 x64 |
| **CPU** | x64 or ARM64 | x64 or ARM64 |
| **RAM** | 50 MB Free RAM | 75 MB Free RAM |
| **.NET** | .NET Framework 4.8.1 | .NET Framework 4.8.1 |

---

## Tipke za bližnjice

| Bližnjica | Ukrep |
|--|--|
| `F1` | Svetla tema |
| `F2` | Temna tema |
| `F3` | Sistemska tema |
| `F4` | Začetek: V oknu |
| `F5` | Začetek: Celoten zaslon |
| `F6` | Varnostna opozorila: Vključeno |
| `F7` | Varnostna opozorila: Izključeno |
| `F8` | Prikrivanje gesel: Vključeno |
| `F9` | Prikrivanje gesel: Izključeno |
| `F10` | Spremeni geslo |
| `F11` | Preveri posodobitve |
| `F12` | O programu |
| `CTRL + P` | Generator gesel |
| `CTRL + Alt + D` | Stran za donacije |
| `CTRL + Shift 1` | Izvozi datoteko Astel |
| `CTRL + Shift 2` | Izvozi datoteko CSV |
| `CTRL + Shift 3` | Uvozi datoteko Astel |
| `CTRL + Shift 4` | Uvozi datoteko CSV |
| `CTRL + Shift 5` | Varnostno kopiranje: Vključeno |
| `CTRL + Shift 6` | Varnostno kopiranje: Izključeno |
| `CTRL + Shift 7` | Odpri mapo z varnostnimi kopijami |
| `ESC` | Počisti izbiro |

---

## Varnost

* **Politika neizvažanja podatkov:** Vaša zasebnost je naša prednostna naloga; nobeni podatki ne zapustijo vaše naprave.
* **Brez odvisnosti:** Program je v celoti razvit od začetka z uporabo lastne izvorne kode, zato ni tveganj zaradi varnostnih ranljivosti v knjižnicah tretjih oseb.
* **Odprta koda:** Celotna izvorna koda programa je odprta in jo lahko pregleda kdorkoli.

---

## Licenca

Ta programska oprema je na voljo brezplačno kot del **paketa rešitev Türkaysoft** in je zaščitena z [**MIT License**](https://github.com/turkaysoft/glow?tab=MIT-1-ov-file).

</details>
