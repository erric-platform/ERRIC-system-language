# Language Description

## Grammar

* **Program** : { Annotation | Data | Module | Routine | Code }
* **Annotation** : `#` Identifier [ AnnotationParams ]
* **AnnotationParams** : `<` { Text } `>`
* **Data** : `data` Identifier [ Literal { `,` Literal } ] `end`
* **Module** : `module` Identifier { VarDeclaration | Routine } `end`
* **Code** : `code` { VarDeclaration | Statement } `end`

---

* **VarDeclaration** : Variable | Constant
* **Variable** : Type VarDefinition { `,` VarDefinition } `;`
* **Type** : `int` | `short` | `byte`
* **VarDefinition** : Identifier [ `:=` Expression ]
* **Constant** : `const` ConstDefinition { `,` ConstDefinition } `;`
* **ConstDefinition** : Identifier `=` Expression

---

* **Routine** : [ Attribute ] `routine` Identifier [ Parameters ] [ Results ] ( `;` | RoutineBody )
* **Attribute** : `start` | `entry`
* **Parameters** : `(` Parameter { `,` Parameter } `)`
* **Parameter** : Type Identifier | Register
* **Results** : `:` Register { `,` Register }
* **RoutineBody** : `do` { VarDeclaration | Statement } `end`
* **Statement** : { Label } ( Directive | AssemblerStatement | ExtensionStatement )
* **Label** : `<` Identifier `>`
* **Directive** : `format` ( 8 | 16 | 32 ) `;`

---

* AssemblerStatement  
&emsp;: `skip` // NOP  
&emsp;| `stop` // STOP  
&emsp;| Register `:=` `*`Register // Rj := \*Ri LD  
&emsp;| Register `:=` Expression // Rj := Const LDA  
&emsp;| `*`Register `:=` Register // \*Rj := Ri ST  
&emsp;| Register `:=` Register // Rj := Ri MOV  
&emsp;| Register `+=` Register // Rj += Ri ADD  
&emsp;| Register `-=` Register // Rj -= Ri SUB  
&emsp;| Register `>>=` Register // Rj >>= Ri ASR  
&emsp;| Register `<<=` Register // Rj <<= Ri ASL  
&emsp;| Register `|=` Register // Rj |= Ri OR  
&emsp;| Register `&=` Register // Rj &= Ri AND  
&emsp;| Register `^=` Register // Rj ^= Ri XOR  
&emsp;| Register `<=` Register // Rj <= Ri LSL  
&emsp;| Register `>=` Register // Rj >= Ri LSR  
&emsp;| Register `?=` Register // Rj ?= Ri CND  
&emsp;| `if` Register `goto` Register // if Ri goto Rj CBR
* **Register** : R0 | R1 | ... | R30 | R31

---

* **ExtensionStatement** : Assignment | Swap | Call | If | Loop | Break | Goto
* **Loop** : For | While | LoopBody
* **For** : `for` Identifier [ `from` Expression ] [ `to` Expression] [ `step` Expression ] LoopBody
* **While** : `while` Expression LoopBody
* **LoopBody** : `loop` BlockBody `end`
* **BlockBody** : { Statement }
* **Break** : `break` `;`
* **Goto** : `goto` Identifier `;`
* **Assignment** : Primary `:=` Expression `;`
* **Swap** : Primary `<=>` Primary `;`

---

* **If** : `if` Expression `do` BlockBody ( `end` | `elif` If | `else` BlockBody `end` ) )
* **Call** : [ Identifier`.` ] Identifier CallParms
* **CallParms** : `(`[ Expression { , Expression } ]`)`

---

* **Expression** : Operand [ Operator Operand ]
* **Operator** : `+` | `-` | `*` | `&` | `|` | `^` | `?` | CompOperator
* **CompOperator** : `=` | `/=` | `<` | `>`
* **Operand** : Receiver | Reference | Literal
* **Primary** : Receiver | Dereference | ExplicitAddress
* **Receiver** : Identifier | ArrayAccess | Register
* **ArrayAccess** : Identifier`[`Expression`]`
* **Reference** : `&` Identifier
* **Dereference** : `*` ( Identifier | Register )
* **ExplicitAddress** : `*` Literal

---

* **Identifier** : *(_a-zA-Z0-9)+*
* **Literal**: *(0-9)+*
* **Text**: *(\\.\\-_a-zA-Z0-9)+*

## Default values

* Default attribute for function is `entry`
* In 'for' loop:
    1. Default `from` is current value is variable exists or 0
    2. Default `to` is infinity, thus the loop would stop only on 'break' command
    3. Default `step` is '1'
* Uninitialized variables can contain any garbage in it.

## Limitations

* Accessed functions and modules has to be already defined. For functions, at least it's interface has to be defined.  
* There is no functions\module overloading.
* `entry` functions has to have function body, `start` functions are declarations of interface and can't have function body.
* Constants can't be changed, thus they can't be used in 'for' loops.
* 'Directive' affects only the next 'Assembler statement'.

## Annotations

// todo

## Comments

Language supports only one-line comments that starts with `//`

## Future plans

[ ] Create functions with different interface  
[ ] Allow multiple breaks at once  