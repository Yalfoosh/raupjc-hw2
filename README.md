# raupjc-hw2
The second homework for FER's C# course. The Solution file is in Z1, sicne that is the startup project.

# Pitanje 1:
5004 ms.

# Pitanje 2:
Jednoj jedinstvenoj, ID => 1.

# Pitanje 3:
1014 ms.

# Pitanje 4:
Pet jedinstvenih, ID => {1, 6, 3, 5, 4}.

# Pitanje 5:
Kada dodijelimo posao dretvi, dodijelimo joj i neki memorijski prostor programa. Ako to izričito ne naglasimo (a u prethodnim primjerima nismo), naše dretve imati će isti zajednički prostor, tj. memorijski prostor određenog lokalnog prostora u programu (npr. metode).

Recimo da pokrećemo 5 dretvi koje će vršiti aritmetičke operacije nad nekom postavljenom varijablom. Proces koji svaka dretva obavlja može se dočarati sljedećim pseudokodom:

T međuspremnik = Dohvati(x);  
return Obradi(međuspremnik);  

No u slučaju više dretvi, operacije relativno na tok vremena nisu sinkrone, tj. izvršavanje dretvi nije sljedno, pa je vjerojatno da će se dogoditi, između ostalog, da zbog sporosti obrade i spremanja vrijednosti dvije različite dretve pročitaju istu vrijednost na varijabli u istoj dijeljenoj memoriji. Tada će obje dretve obaviti svoju obradu te naizmjence pohraniti rezultat, što je kao da smo sve različite dretve kojima smo dali posao osim one koja uspije obaviti posao zapravo ušutkali.
