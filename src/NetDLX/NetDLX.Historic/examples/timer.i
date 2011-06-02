;-----------------------------------------------------------------------;
;                                                                       ;
;       DLX Timer Simulation Header - D. J. Viner                       ;
;                                                                       ;
;                                                                       ;
;       Updates                                                         ;
; DJV   04.08.95    Created from C coding in cpu.h                      ;
;                                                                       ;
;-----------------------------------------------------------------------;
;                                                                       ;
; This module provides a header for the simulated timers. Three         ;
; identical and simple timers are provided each with three registers    ;
; which are located at the addresses defined below. Two methods are     ;
; provided to access timer registers - either directly or via a base    ;
; pointer and offsets.                                                  ;
;                                                                       ;
; To use a timer, load the latch with the required count. Note that     ;
; this operation will automatically zero the count register. The count  ;
; register cannot be written to directly. To start a timer set its      ;
; ENABLE bit by ORing in T_ENABLED to its status register.              ;
;                                                                       ;
; Once a timer count has reached the value in its latch it will cause a ;
; level 4 interrupt to the cpu and set the interrupt bit in the status  ;
; register. To determine which of the three timers caused the interrupt ;
; just read the status register. Note that reading a status register    ;
; automatically clears the interrupt flag.                              ;
;                                                                       ;
;-----------------------------------------------------------------------;

        NODEBUG     ; Don't debug or list this include
        NOLIST


        ; Timer register absolute addresses

        T1_LATCH    EQU     $FF000000
        T1_COUNT    EQU     $FF000004
        T1_STATUS   EQU     $FF000008

        T2_LATCH    EQU     $FF000010
        T2_COUNT    EQU     $FF000014
        T2_STATUS   EQU     $FF000018

        T3_LATCH    EQU     $FF000020
        T3_COUNT    EQU     $FF000024
        T3_STATUS   EQU     $FF000028


        ; Timer register base and offsets

        TIMER_BASE  EQU     $FF000000

        TO1_LATCH   EQU     $00
        TO1_COUNT   EQU     $04
        TO1_STATUS  EQU     $08

        TO2_LATCH   EQU     $10
        TO2_COUNT   EQU     $14
        TO2_STATUS  EQU     $18

        TO3_LATCH   EQU     $20
        TO3_COUNT   EQU     $24
        TO3_STATUS  EQU     $28


        ; Timer status bits

        T_ENABLED   EQU     $00000001      ; Bit 0
        T_INTERRUPT EQU     $00000080      ; Bit 7


;-----------------------------------------------------------------------;

        DEBUG
        LIST

;-----------------------------------------------------------------------;



