module Holo
   module Type =
      type Primitive =
         | Byte
         | Word
         | Dword

   type Type =
      | Void
      | Primitive of Type.Primitive
      | Ptr of Type
      | Bound of string

   module Storage =
      type Register =
         | EAX
         | EBX
         | ECX

   type Storage =
      | AllocateOnStack
      | Register of Storage.Register

   [<Struct>]
   type Holo = {Type: Type; Storage: Storage}
