.386
.model flat, stdcall
option casemap: none

.data
   $vyd_s byte "Hello, World", 0

.code
   .main:
      mov edx, offset $vyd_s
      invoke GetStdHandle, STD_OUTPUT_HANDLE
      invoke ExitProcess, 0
end main
