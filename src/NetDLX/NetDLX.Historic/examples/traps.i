;-----------------------------------------------------------------------;
;                                                                       ;
;       DLX Trap Routines - D. J. Viner                                 ;
;                                                                       ;
;                                                                       ;
;       Updates                                                         ;
; DJV   16.07.95    Created                                             ;
; DJV   18.07.95    Added overflow                                      ;
; DJV   23.10.95    L32O mods added and PADs removed                    ;
;                                                                       ;
;                                                                       ;
; INCLUDE this file at the end of main code and call JAL InitTraps near ;
; the beginning. All divide by zero and unimplemented instructions will ;
; then be caught.                                                       ;
;                                                                       ;
; Requires macro.i to have been INCLUDEd previously and print.i to be   ;
; included somewhere                                                    ;
;                                                                       ;
;-----------------------------------------------------------------------;

            NODEBUG     ; Don't debug or list this include
            NOLIST

;-----------------------------------------------------------------------;
; InitTraps                                                             ;
;                                                                       ;
; Initialise the trap vectors                                           ;
;-----------------------------------------------------------------------;

InitTraps   L32O    R1,TrapUnimp    ; Initialise register to int code
                                    ; for Unimplemented Instruction
            SW      4(R0),R1        ; ...and store in vector

            L32O    R1,TrapDiv      ; Same for Divide By Zero...
            SW      8(R0),R1
            L32O    R1,TrapOverflow ; ...and overflow
            SW      12(R0),R1

            JR      R31


TrapUnimp   JAL     PRINT
            DC.B    'Unimplemented instruction error',10,0
            BRA     TrapOut

TrapDiv     JAL     PRINT
            DC.B    'Division by zero error',10,0
            BRA     TrapOut

TrapOverflow
            JAL     PRINT
            DC.B    'Integer overflow error',10,0

TrapOut     MOVS2I  R1,S0           ; Get saved address from IAR
            SUBUI   R1,R1,#4        ; Point to instr that caused error
            JAL     PRINT
            DC.B    'Error location: $',0
            OUTPUTHEX               ; Output address in R1
            JAL     PRINT
            DC.B    10,0

            HALT


;-----------------------------------------------------------------------;

            DEBUG       ; Turn on any debugging/listing as set by
            LIST        ; the DASM command line

;-----------------------------------------------------------------------;




