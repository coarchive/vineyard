module Lexer
open System.Text
open System.Collections.Generic

type Lexeme =
   | ParenLeft
   | ParenRight
   | BraceOpen
   | BraceClose
   | Equals
   | Colon
   | Caret
   | Let
   | In
   | Percent
   | Byte
   | Word
   | Dword
   | Instrinsic
   | NewType
   | Extern
   | BareWord of string
   | String of string

type LexerState =
   | Normal
   | String
   // | Char
   // | Escaped

let lex (input: string array): Lexeme List =
   let out = List<Lexeme>()

   let state  = Stack<LexerState>()
   let levels = Stack<uint>()

   let mutable i = 0

   state.Push Normal
   levels.Push 0u

   while i < input.Length do
      match state.Peek() with
      | Normal ->
         let token = input.[i]
         match token with
         | "\""  -> state.Push String
         | " "  -> ()
         | "\n" -> ()
         | "\t" -> ()
         | "(" -> out.Add ParenLeft
         | ")" -> out.Add ParenRight
         | "{" -> out.Add BraceOpen
         | "}" -> out.Add BraceClose
         | "=" -> out.Add Equals
         | ":" -> out.Add Colon
         | "^" -> out.Add Caret
         | "%" -> out.Add Percent
         | "in" -> out.Add In
         | "let" -> out.Add Let
         | "byte" -> out.Add Byte
         | "word" -> out.Add Word
         | "dword" -> out.Add Dword
         | "extern" -> out.Add Extern
         | "newtype" -> out.Add NewType
         | "intrinsic" -> out.Add Instrinsic
         | word  -> out.Add (BareWord word)
         i <- i + 1
      | String ->
         let workingString = StringBuilder()
         while input.[i] <> "\"" do
            workingString.Append input.[i] |> ignore
            i <- i + 1
         // currently, i rests on the end quote
         out.Add(Lexeme.String (workingString.ToString()))
         i <- i + 1
         state.Pop() |> ignore
   out
