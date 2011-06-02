;-----------------------------------------------------------------------;
;                                                                       ;
;       Print Routine - D. J. Viner                                     ;
;                                                                       ;
;                                                                       ;
;       Updates                                                         ;
; DJV   12.07.95    Created from original PRINT macro in macro.i        ;
; DJV   06.08.95    Added Status Reg save coding to prevent interrupts  ;
;                   from disrupting the output                          ;
; DJV   23.10.95    Removed PAD information as DAsm can now insert      ;
;                   PADs automatically                                  ;
;                                                                       ;
;-----------------------------------------------------------------------;
; NOTE: INCLUDE this file near the end of main code.                    ;
;-----------------------------------------------------------------------;

        NODEBUG     ; Don't debug or list this include
        NOLIST

;-----------------------------------------------------------------------;
; Print the null-terminated string following the call. Note that R30    ;
; must be initialised as a stack pointer before this call as PUSH/PULL  ;
; is used in order to save and restore the contents of R1. R29 must     ;
; also be initialised to point to the current load offset. (Both R30    ;
; and R29 will be set correctly if macro.i has been INCLUDEd first.     ;
;                                                                       ;
;       Format:     JAL     PRINT                                       ;
;                   DC.B    'Some text',0                               ;
;-----------------------------------------------------------------------;


PRINT   PUSH    R1              ; Be kind and save R1 contents
        MOVS2I  R1,S1           ; Get status register
        SW      PTemp(R29),R1   ; Save it
        ORI     R1,R1,#%111000  ; Set NMI
        MOVI2S  S1,R1           ; Put in status reg

PLoop   LBU     R1,0(R31)       ; Get byte to print
        BEQZ    R1,POut         ; Exit loop if 0 found
        OUTPUTBYTE              ; Print contents of R1
        INC     R31             ; Inc R31 to point to next byte
        BRA     PLoop           ; Always branch

POut    SRLI    R31,R31,#2      ; This code makes sure that R31 points
        SLLI    R31,R31,#2      ; to the first long-word aligned
        ADDUI   R31,R31,#4      ; address after the end of the text

        LW      R1,PTemp(R29)   ; Restore original status reg
        MOVI2S  S1,R1

        PULL    R1              ; Restore original contents of R1

        JR      R31             ; Jump to code following text

PTemp   DC.L    0               ; Temporary storage for status reg

;-----------------------------------------------------------------------;

        DEBUG       ; Turn on any debugging/listing as set by
        LIST        ; the DASM command line

;-----------------------------------------------------------------------;



