# Aplikacja testowa

## Wersja demo
Aplikacja znajduje się pod adresem [https://swsw.somee.com/](https://swsw.somee.com) <br>


## Autoryzacja
Aby wejść do zabezpieczonych stron, należy się potwierdzić tożsamość(Logowanie z Microsoft) <br>

## Pobieranie kursów walut
Pod adresem [Home/Currency](https://swsw.somee.com/Home/Currency) aplikacja pobiera aktualny kurs walut ze strony [http://api.nbp.pl/](http://api.nbp.pl/)

> [!TIP]
> Aplikacja korzysta z IMemoryCache. Dane z NBP trzymane są w cache przez 5 minut
 
 <br>

## Wysyłanie mail

Pod adresem [Home/Mail](https://swsw.somee.com/Home/Mail) znajduje się formualrz do wysłania maila z polami
- Temat
- Treść
- Do kogo(jeśli nie wypełnimy to adresat pobrany bedzię z pliku [appsettings.json](https://github.com/swdowia1/Aplikacja8/blob/master/WebApp/appsettings.json)&rarr;MailTo)

<br>
Dane do konfigarcji klienta pocztowego znajdują się [tutaj](https://github.com/swdowia1/Aplikacja8/blob/master/WebApp/appsettings.json) &rarr;MailSetting



