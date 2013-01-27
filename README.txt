==== Scrappy Compiler ====

Scrappy kompilátor byl vyvinut za použití GOLD Parseru[0] využívají .NET engine bsn-goldparser[1]. Kompilátor tedy je napsán v .NET v jazyce C#. Gramatika byla napsána  v GOLD Parser syntaxi a následně přeložena a vytvořena nezbytná mapování na jednotlivé uzly gramatiky. Jakmile proběhne namapování, tak proběhne kompilace.

Ke spuštění je potřeba Windows s .NET 4.5 a ke kompilaci Visual Studio 2012. Mohou fungovat i nižší verze, ale nebylo testováno. Rovněž je možné spustit na jiných platformách. Testováno a částečně vyvíjeno bylo na Mac OS X s Mono 3.0[2] a MonoDevelop 3.0[3], což vyžadovalo provést úpravu ve verzi bsn-goldparser enginu kvůli nekompatibilitě. Chyba byla nahlášena autorovi[4]. Mělo by to fungovat i na jiných platformách, kde funguje Mono.

V případě, že chcete projekt zkompilovat, tak stačí otevřít Scrappy.sln v daném vývojovém prostředí a spustit kompilaci. Následně vývojové prostředí výsledek i pustí. Pokud nechcete kompilovat, tak součástí je zkompilována verze, kterou lze spustit v případě Windows:

Zkompilováná verze je v Scrappy/bin/Release/Scrappy.exe

$ Scrappy.exe Examples/Knapsack.sp

V případě Mono:

$ mono Scrappy.exe Examples/Knapsack.sp

[0] : http://goldparser.org/
[1] : http://code.google.com/p/bsn-goldparser/
[2] : http://www.go-mono.com/mono-downloads/download.html
[3] : http://monodevelop.com/Download
[4] : http://code.google.com/p/bsn-goldparser/issues/detail?id=8

=== Změny v nové verzi ===

Přibyla podpora pro dědičnost a metody jsou spouštěny pomocí dynamic dispatch a tím pádem kompilátor již neověřuje existence metod. Plus jsou při dědičnosti zkopírovány fieldy do potomků, které dědí z dané třídy.

Třída, která chce dědit se zapisuje

```
class B : A

-- something

end
```

Pokud není uvedeno odkud dědí, tak dědí z Any.

Když chci použít rodiče, tak volám parent::xxx()

Když chci volat vyššího rodiče, tak uvedu název A::xxx()

V instrukcí to teď vypadá takto 
<instruction>invokevirtual Instance::New::3</instruction>

Instance tam nemusí být v případě, že není v kódu určena třída, na které se má metoda volat. Číslo značí počet argumentů.