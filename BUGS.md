# BUGS

The starter contains behaviors that contradict `ASSIGNMENT.md`. Write here every discrepancy you found.
Fill it in yourself, in your own words. There is no required number: what counts is that they are real.

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
|R6|BookingService, liniile de inceput, nu se valida durata intervalului|am citit ce scria in R6 si cand am verificat ce a facut, mi-am dat seama| am dat un prompt la ai sa-mi mute validarea unde trebuia|
|R8|BookingService, liniile 58-62 se seta mereu cancelled, inclusiv la o rezervare deja anulata|Citind metoda Cancel si cazul unei a doua apelari |Booking.Cancel() returneaza conflict daca statusul este deja cancelled  |