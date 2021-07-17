.386
.model flat, stdcall
option casemap: none

include <Irvine32.inc>
includelib <Irvine32.Lib>

.data
   $irv_greeting byte "Hello, World", 0
   $irv_greeting$length dword 12

.code
.main:
   mov edx, offset $irv_greeting
   call WriteString
   invoke ExitProcess, 0
end main
