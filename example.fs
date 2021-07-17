type Storage = Memory

type add =
   Integral 'a, Storage 's0 's1 => 
let add a b =
   asm "add" a b

typ WriteString = u8^ in eax -> ()

typ FizzBuzz = u32 -> ()
FizzBuzz = n ->
   typ i = u32 in ecx
   let i = 0

   while i > 0
      ...

   typ divisibleBy3 = bool
   let divisibleBy3 = 
