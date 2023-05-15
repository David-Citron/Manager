# O hře
3D hra vytvořena pomocí herního enginu Unity. Hlavním motivem hry je správa restaurace a rozdávání příkazů zaměstnanci. Hráč může hru ovládat myší nebo pomocí klávesnice. Klávesové zkratky pro každou akci jsou zobrazeny na obrazovce. Pohyb hráče spravuje navigační systém, který je přednastavený v enginu Unity. Hra je opatřena originálním soundtrackem.
# Návody pro hráče
Všechny potřebné návody pro nového hráče.

## Ovládání hráče
Veškerý pohyb hráče je ovládán pomocí tlačítek znázorněných na herním poli. Můžeme buď mačkat přiřazené klávesové zkratky nebo klikat na samotné tlačítka. Pokud se přiblížíme jednomu ze stanovených stanovišť, tak se nám nad inventářem zobrazí tlačítko pro interakce. Toto funguje stejným způsobem.

## Jak obsloužit zákazníka
1. Prvně se ujistíme, že máme přichystaný tácek. Pokud ne tak půjdeme k regálu. Vezmeme zásoby a přineseme je na místo označené pomocí klávesové zkratky "F".
2. Půjdeme k lednici, z které vezmeme syrové maso. Doneseme jej ke grilu. Tam ho následně položíme.
3. Po deseti sekundách se maso osmaží.
4. Hotové maso přineseme a položíme na tác.
5. Celý tác následně vezmeme a přineseme zákazníkovi.
6. Následujících 20 sekund ho bude zákazník konzumovat. Tento čas lze využít k přípravě dalšího jídla.
7. Po této době příjde ke kase, pokud u ní bude volno. Zákazník počká, než ke kase přijde hráč a umožní mu zaplatit.

## Doplňování zásob
1. V pravém horním rohu klikneme na první záložku.
2. Pokud máme dostatečně peněz a žádný kurýr není na mapě, tak si můžeme objednat zásoby do regálu.
3. Co nejdříve se objeví kurýr, který příjde k regálu a vyskladní tam zboží.
4. Jakmile bude hotov, tak odejde a budeme si moct zavolat dalšího.

Poznámka: Jedna objednávka = 4 zásoby

## Hudební přehrávač
1. V pravém horním rohu klikneme na druhou záložku.
2. Zobrazí se panel s písničkama a pod ním přehrávač.
3. Pokud chceme pustit konkrétní písničku, tak klikneme na její ikonku v horním panelu.
4. Přehrávač obsahuje 3 tlačítka. Jedno pro opakování stávající skladby. Druhé slouží pro pozastavení a nebo pokračování skladby. Poslední tlačítko určuje, zda je zvuk zapnutý nebo vypnutý.

# Detailní popis herních mechanik
## Návštěvníci
Pokud jsou stoly na mapě prázdné, tak se objeví dva noví zákazníci, kteří si sednou k prvnímu neobsazenému stolu. Čekají na to, že jim hráč přinese jejich objednávku. Po obdržení jejich jídla, jej budou 20 sekund konzumovat. Následně se jeden ze dvou návštěvníků vydá k pokladně. Pokud je u pokladny obsazeno, tak budou čekat, než se tam místo neuvolní. Poté co zákazník zaplatí, tak se přičte hráči 100$. Oba zákazníci odejdou a po nějaké době se objeví další.

## Hráč
Hráč se pohybuje pomocí klikání na podlahu levým tlačítkem myši a nebo pomocí mačkání klávesových zkratek. Poslední alternativou je klikání myší na tlačítka nacházející se na mapě. Pokud model hráče dojde k jednomu z hitboxů, tak se mu objeví možnost interagovat s daným místem.

Poznámka: Hráč má v navigačním systému nastavenou vyšší prioritu, než kurýr a zákazníci

## Lednice
Pokud hráč příjde k hitboxu lednice, zmáčkne tlačítko interakce a má prázdný inventář, tak obdrží do svého inventáře syrové maso.

## Gril
Jestli se hráč nachází v hitboxu grilu, tak může nastat několik situací poté, co zmáčkne tlačítko pro interakci.
1. Pokládání masa - Hráč musí mít v inventáři syrové maso, to mu bude odebráno. Na 3D modelu se objeví maso.
2. Obdržení uvařeného masa - Hráč musí mít prázdný inventář. Obdrží uvařené maso a zmizí jeho model z grilu.
3. Obdržení spáleného masa - Hráč musí mít prázdný inventář. Stejně jako u uvařeného masa získá spálené maso a model zmizí, ale tento stav masa nemá využití.

## Regál
Regál má speciální systém pro zobrazování 3D modelů. Čtyři zásoby odpovídají jednomu 3D modelu boxu. V praxi to znamená tak, že pokud nemáme žádné zásoby, tak se nezobrazí žádný model boxu na policích. Jestli máme jen jednu, tak se zobrazí jeden box. Pokud máme 4, tak je zobrazen stále jen 1 box. Jakmile budeme mít ale už 5, tak se zobrazí druhý.
Pokud máme nějaké zásoby, prázdný inventář a chceme interagovat s regálem, tak si jednu vezmeme do inventáře. Můžeme ji následně použít u tácku.
Další možností je objednání nových zásob. To je možné pokud není žádný kurýr na mapě a máme alespoň 100$. Tuto možnost lze nalézt v první záložce v levém horním rohu.

## Tácek
Při interakci může nastat několik možností.
1. Obnovíme stav (tj. tácek se znovu zobrazí) - Musíme mít v inventáři zásoby.
2. Přidáme ingerienci - Pokud to lze, tak se pridá ingredience, aktualizuje se model a z inventáře zmizí předmět.
3. Vezmeme tácek - Pokud máme prázdný inventář a na tácku jsou všechny ingredience, tak jej můžeme vzít do inventáře. Model zmizí a pro opětovné použití tácku musíme přinést zásoby.

## Kasa
Pokud čeká zákazník u kasy a hráč bude s kasou interagovat, tak zákazník zaplatí 100$. Následně odejde.

## Inventář
Inventář má kapacitu jen pro jeden předmět. Při každé změně se vykreslí znovu i textura. Nikdy se nemůže stát, že by se předmět přidal, pokud není v inventáři volné místo. Pro odstranění předmětu z inventáře stačí zmáčknout tlačítko "X" a nebo jeho tlačítko v pravém dolním rohu inventáře. 

## Hudební přehrávač
Hudební přehráváč automaticky spouští hudbu. Pokud jedna písnička skončí, tak se spustí druhá. Toto neplatí pokud si hráč zapne opakování skladby, poté se skladba spustí od začátku. Hráč si také může ztlumit zvuk a zastavit písničku. Pokud chce v poslechu pokračovat, tak stačí jen znovu zmáčknout tlačítko uprostřed hudebního přehrávače. Pro spuštění konkrétní skladby může hráč kliknout na tlačítka vedle názvů skladeb.

## Peníze
V pravém horním rohu se nachází text zobrazující aktuální počet peněz. Peníze se zvýší, pokud hráč obslouží zákazníka. Naopak se sníží, pokud si koupí zásoby.

# Stav vývoje
Aktuální stav vývoje hry.
## 3D modely
- [x] Kasa
- [x] Grill
- [x] Regál
- [x] Lednice
- [x] Talíř
- [x] Plastová láhev na kečup a hořčici
- [x] Dřevěná bedna
- [x] Stůl
- [x] Maso
- [x] Burger
- [x] Houska
## 2D textury předmětů
- [x] Surové maso
- [x] Osmažené maso
- [x] Spálené maso
- [x] Dřevěná bedna
- [x] Burger
## Mechaniky
- [x] Pohyb zaměstnance
- [x] Systém inventáře
- [x] Systém správného ukazování zásob v regálu
- [x] Získání surového masa
- [x] Průběh připravování masa
- [x] Zobrazování časovače při přípravě masa
- [x] Získání zásob z regálu
- [x] Dokončení výroby burgeru
- [x] Objednávky nových zásob
- [x] Doplňování zásob do regálu
- [x] Systém zákazníků
- [x] Placení zákazníků
- [x] Ukazatel peněz
## Ostatní
- [x] Soundtrack
