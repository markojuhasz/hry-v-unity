# Stručný popis ku hrám, ktoré som vytvoril v Unity.

# DONGPONG 2D ![dongPong3](https://github.com/user-attachments/assets/a2727b80-84fc-4dc8-ad21-080417c12696)

- [Odkaz na hru](https://play.unity.com/en/games/34e450e7-27f6-4176-b79e-44cbf0abccfd/dongpong)
- z kociek som vytvoril 2 pádla, pridal guličku a zadal maximálnu hraciu plochu
- spravil som Input Mapping z nového input systému pre ovládanie ( W/S, Up/Down )
- pádlam som pridal Rigidbody2D, vytvoril kód pre pohyb pádel ( W/S lavá strana; Up/Down pravá strana; )
- na guličku som pridal Rigidbody2D, vytvoril script a začal písať
- tu som sa zasekol, chcel som spraviť aby padla na zem a po chvíli sa odrazila do roznej pozicie
- využil som chatGPT aby mi poradil a spravil som script
- pridal som logiku scóre, gameManager, spravil UI
- vytvoril som tlačítka pre Start, Exit, Pause, Reset
- ak hra dosiahne skóre 10, hráč ju môže ukončiť alebo resetovať 
- [Screenshoty](dongpong2D/screenshots/)
- [Script](dongpong2D/script/)

# Jetpack Hustler ![jtpckHustler](https://github.com/user-attachments/assets/b3e21cae-cd86-4b56-b7cc-5d68d23e9b99)


- [Odkaz na hru](https://marecheckk.itch.io/jetpack-hustler)
- začal som spísaním si vlastného Game Design Documentu
- hlavným cielom bolo vytvoriť 2D platformer, kde hráč skáče cez prekážky, zbiera objekty a sem - tam rieši malé puzzle ( posunúť box aby doskočil )
- nakreslil som si level design pomocou kockového papiera, štvorcov a začal skladať level
- keď bol level hotový, chcel som mať pohyb podobný platformovkám, ktoré som mal rád .. takže som spravil research
- variabilný jump, max jump height, max fall speed a rozbeh pomocou akcelerácie ( na použitie coyote timer som neprišiel, ale v kóde zostal )
- v programe Kryta som nakreslil 2D sprites pre platformy, collectibles a postavy
- pridal som hudbu, vytvoril zvuky a pridal do hry ( väčšinou na collider, trigger alebo input )
- vytvoril som scénu, ktorá hráčovi ukáže ako sa hra ovláda a čo obsahuje
- dokončil som menu, tlačítka a spravil poslednú scénu ( skúšal som pohyblivé titulky )
- [Sprites](jetpackHustler/sprites)
- [Screenshoty](jetpackHustler/screenshots)
- [Script](jetpackHustler/script)
 
# The Mistfall ( StreamAlly Comunity Game Jam ) ![Mistfall nahlad](https://github.com/user-attachments/assets/5ffd589c-c69f-4a6e-8bf9-c4a9050bb300)

- [Odkaz na hru](https://marecheckk.itch.io/the-mistfall)
- prihlásil som sa do Game Jamu a pridal sa do CZ/SK GameDev Comunity 
- kedže som sa nedostal do hlavného Jamu kvôli zaplneným kapacitám, nestratil som nádej, zapálenie pre túto akciu a pridal som sa do komunitnej verzie na stránke Itch.io
- <mark>Téma Jamu</mark>: You shouldn´t be here
- <mark>Môj nápad</mark>: Hráč sa pri nočnej prechádzke ocitol v blúdnom kruhu začarovaného lesa a musí vyriešiť záhadu aby sa dostal von
- zadanie záhady znie: zničiť všetky predmety viazané ku strateným dušiam
- každý predmet stráži stratená duša a keď sa hráč dostane bližšie, nezasvieti na entity baterkou, duše ho prenasledujú a môžu zabiť
- dosiahnuté cez Navmesh, raycast a 2 staty ( stráž, prenasleduj ) 
- po vyzbieraní všetkých predmetov hra končí poďakovaním 
- <mark>čo som vytvoril</mark>: 250x250m terén tmavého lesa s hmlou, aby som pozdvihol atmosféru
- využil som unity free 3D assety ktoré som rozmiestnil po mape a vytvoril z nich zbieratelne predmety
- hráč je vlastne kapsula s kamerou ktorá pôsobí ako FirstPerson mode
- pridal som baterku a začal písať skript
- po skripte som pridal knihu, ktorú hráč vyzdvihne na začiatku hry a pvysvetlí princíp
- dokončil som UI, ktoré je svojím spôsobom interaktívne / aktívne
- pri zničení predmetov sa zdvihnutý predmet markne ( podľa počtu predmetov.. 1 = mark 0 atd )
- [Screenshoty](TheMistfall/screenshots)
- [Script](TheMistfall/script)
