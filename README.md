# Stručný popis ku hrám, ktoré som vytvoril v Unity.

# DONGPONG 2D 
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

  # Jetpack Hustler
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
