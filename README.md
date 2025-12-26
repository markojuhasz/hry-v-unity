# 📝Stručný popis ku hrám, ktoré som vytvoril v Unity.

## 🎮DONGPONG 2D ![dongPong3](https://github.com/user-attachments/assets/a2727b80-84fc-4dc8-ad21-080417c12696)

- [Odkaz na hru](https://play.unity.com/en/games/34e450e7-27f6-4176-b79e-44cbf0abccfd/dongpong)
- scéna z primitívnych objektov ( gulička, pádla(hráč), maxBound )
- input na báze readValue<float> pre ovládanie
- script pre pohyb pádel ( W/S lavá strana; Up/Down pravá strana; )
- gulička je ovplyvnená fyzikou 
- pri štarte alebo respawne sa gulička dotkne spodnej hranice a vyletí do náhodnej strany
- ak loptička preletí za hráča, protilahlý hráč skóruje a loptička sa respawne
- počítanie skóre -> hra je ovplyvnená logikou ( jeden hráč dosiahne skóre 10, hra končí )
- UI: Start, Exit, Pause(Esc), Continue a možný Reset po skončení
- [Screenshoty](dongpong2D/screenshots/)
- [Script](dongpong2D/script/)

## 🎮Jetpack Hustler ![jtpckHustler](https://github.com/user-attachments/assets/b3e21cae-cd86-4b56-b7cc-5d68d23e9b99)

- [Odkaz na hru](https://marecheckk.itch.io/jetpack-hustler)
- 2D platformer, hráč skáče cez prekážky, zbiera objekty a sem - tam rieši malé puzzle ( posunúť box aby doskočil )
- nakreslil som Level Design pomocou kockového papiera, štvorcov a začal skladať level
- keď bol level hotový, chcel som mať pohyb podobný platformovkám, ktoré mám rád.. takže som spravil research
- variabilný jump, max jump height, max fall speed, max move speed a rozbeh pomocou akcelerácie
- ( na použitie coyote timer som neprišiel, ale v kóde zostal )
- v programe Krita som nakreslil 2D sprites pre platformy, collectibles a postavy
- pridal som hudbu a vytvoril zvuky ( zvuky sú väčšinou spustené cez trigger, collider alebo input )
- hra je vytvorená zo štyroch scén ( Main Menu, First Level, Main Level, End )
- Main Level obsahuje start - exit button, hudbu a animáciu
- First Level ukáže hráčovi ovládanie, obsah a princíp 
- Main Level obsahuje hlavné prvky -> podľa Game Design Doc a Level Design Doc
- End Level -> po ukončení levelu a vyzbieraní aspon 1500 cash hra končí titulkami
- [Sprites](jetpackHustler/sprites)
- [Screenshoty](jetpackHustler/screenshots)
- [Script](jetpackHustler/script)
 
## 🎮The Mistfall ( StreamAlly Comunity Game Jam ) ![Mistfall nahlad](https://github.com/user-attachments/assets/5ffd589c-c69f-4a6e-8bf9-c4a9050bb300)

- [Odkaz na hru](https://marecheckk.itch.io/the-mistfall)
- prihlásil som sa do Game Jamu a pridal sa do CZ/SK GameDev Comunity 
- nedostal som možnosť pracovať v tíme kvôli zaplneným kapacitám, takže zostala možnosť komunitného Jamu na stránke Itch.io
- <mark>Téma Jamu</mark>: You shouldn´t be here
- <mark>Môj nápad</mark>: Postava pri nočnej prechádzke zablúdila v začarovanom lese a musí vyriešiť záhadu, aby sa dostala von
- Úloha: nájsť a zničiť všetky predmety viazané ku strateným dušiam
- každý predmet stráži stratená duša.. hráč musí na entity svietiť baterkou, inak ho budú prenasledovať a následne zabijú
- dosiahnuté cez Navmesh, raycast a 2 staty ( stráž, prenasleduj ) 
- po zničení všetkých predmetov hra končí
- <mark>Čo som vytvoril</mark>: 250x250m terén tmavého lesa s hmlou, pre atmosféru
- hráč je kapsula s camera componentom, ktorá pôsobí ako FirstPerson mode
- interaktívna baterka ( on/off ), svetlo "vyžaruje" raycast 
- využil som Unity free 3D assety ktoré som rozmiestnil po teréne a z niektorých vytvoril predmety
- Prvý quest: zdvihni knihu ( v knihe má hráč nápovedu )
- kniha sa zapíše ( isCollected ) -> hráč ju môže otvárať / zatvárať 
- Druhý quest: Nájdi a znič predmety ( predmety budú vypísané v hornej časti obrazovky )
- pri zničení predmetu bude predmet marknutý ( podľa počtu predmetov.. predmet 1 = mark [0] )
- [Screenshoty](TheMistfall/screenshots)
- [Script](TheMistfall/script)
