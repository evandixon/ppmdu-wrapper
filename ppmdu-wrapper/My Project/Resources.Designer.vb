﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("PPMDU.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to STANDARD
        '''
        '''GRIN
        '''
        '''PAINED
        '''
        '''ANGRY
        '''
        '''WORRIED
        '''
        '''SAD
        '''
        '''CRYING
        '''
        '''SHOUTING
        '''
        '''TEARY_EYED
        '''
        '''DETERMINED
        '''
        '''JOYOUS
        '''
        '''INSPIRED
        '''
        '''SURPRISED
        '''
        '''DIZZY
        ''' 
        ''' 
        ''' 
        '''
        ''' 
        '''SIGH
        '''
        '''STUNNED.
        '''</summary>
        Friend ReadOnly Property facenames() As String
            Get
                Return ResourceManager.GetString("facenames", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;PMD2&gt;
        '''  &lt;!--=======================================================================--&gt;
        '''  &lt;!--PPMDU Configuration File--&gt;
        '''  &lt;!--=======================================================================--&gt;
        '''  &lt;!--This file is meant to contain all the data that the program uses --&gt;
        '''  &lt;!--at runtime that could be considered version specific, or that might--&gt;
        '''  &lt;!--change at one point.--&gt;
        '''  
        '''  &lt;!--Layout:--&gt;
        '''  &lt;!--Its made of the following structure this far : --&gt;
        '''  &lt;!--&lt;PMD2&gt;--&gt;
        '''  &lt;!--  &lt;GameEditions /&gt;--&gt;
        '''  &lt;!-- [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property pmd2data() As String
            Get
                Return ResourceManager.GetString("pmd2data", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        '''&lt;DSEConversionData&gt;
        '''
        '''&lt;!--
        '''This is an example file to demonstrate how to make your own for your game of choice.
        '''
        '''ABOUT:
        '''    Basically, what this does is tell the program what midi preset to give each to each of the internal in-game instrument presets.
        '''    This is useful, because most of the time, the presets in the games do not match a specific midi preset. So a trumpet in a game might 
        '''    be a bagpipe in midi for example! 
        '''
        '''    And it gets even worse with drumkits. Th [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property pmd2eos_cvinfo() As String
            Get
                Return ResourceManager.GetString("pmd2eos_cvinfo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;PMD2&gt;
        '''  &lt;!-- Contains reference data from the game --&gt;
        '''  &lt;ScriptData&gt;
        '''    
        '''    &lt;!--Common to all 3--&gt;
        '''    &lt;Game id=&quot;EoS_NA&quot; id2=&quot;EoS_EU&quot; id3=&quot;EoS_JP&quot;&gt;
        '''    
        '''      &lt;!--**********************************************--&gt;
        '''      &lt;!--Game Variables Data--&gt;
        '''      &lt;!--**********************************************--&gt;
        '''      &lt;GameVariablesTable&gt;
        '''        &lt;GameVar type=&quot;8&quot; unk1=&quot;2&quot; memoffset=&quot;  0x0&quot; bitshift=&quot;0x0&quot; unk3=&quot;  0x1&quot; unk4=&quot;0x1&quot; name=&quot;VERSION&quot; /&gt;
        '''        &lt;GameVar type=&quot;8&quot; unk1=&quot;2&quot; memoffset=&quot;  0x4&quot; bitshift=&quot;0x [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property pmd2scriptdata() As String
            Get
                Return ResourceManager.GetString("pmd2scriptdata", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to 
        '''bulbasaur
        '''ivysaur
        '''venusaur
        '''charmander
        '''charmeleon
        '''charizard
        '''squirtle
        '''wartortle
        '''blastoise
        '''caterpie
        '''metapod
        '''butterfree
        '''weedle
        '''kakuna
        '''beedrill
        '''pidgey
        '''pidgeotto
        '''pidgeot
        '''rattata
        '''raticate
        '''spearow
        '''fearow
        '''ekans
        '''arbok
        '''pikachu
        '''raichu
        '''sandshrew
        '''sandslash
        '''nidoran-f
        '''nidorina
        '''nidoqueen
        '''nidoran-m
        '''nidorino
        '''nidoking
        '''clefairy
        '''clefable
        '''vulpix
        '''ninetales
        '''jigglypuff
        '''wigglytuff
        '''zubat
        '''golbat
        '''oddish
        '''gloom
        '''vileplume
        '''paras
        '''parasect
        '''venonat
        '''venomoth
        '''diglett
        '''dugtrio
        '''meowth
        '''persian
        '''psyduck
        '''golduck
        '''mankey
        '''primeape
        '''growlithe
        '''arcanine
        '''poli [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property pokenames() As String
            Get
                Return ResourceManager.GetString("pokenames", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_audioutil() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_audioutil", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_dopx() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_dopx", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_kaoutil() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_kaoutil", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_packfileutil() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_packfileutil", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_palettetool() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_palettetool", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_statsutil() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_statsutil", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property ppmd_unpx() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ppmd_unpx", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
    End Module
End Namespace
