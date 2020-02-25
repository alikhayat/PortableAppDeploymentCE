Imports OpenNETCF
Imports OpenNETCF.WindowsCE
Imports System.Threading
Imports System.Threading.Timer
Imports Symbol.Fusion.WLAN
Imports Symbol.Fusion

Public Module PowerManagement
    Dim CallBack As TimerCallback = New TimerCallback(AddressOf ResetIdleTimer)
    Public ThreadTimer As System.Threading.Timer
    Public Sub InializeTimer()
        ThreadTimer = New System.Threading.Timer(CallBack, Nothing, 10000, 10000)
    End Sub
    Private Sub ResetIdleTimer()
        OpenNETCF.WindowsCE.PowerManagement.ResetSystemIdleTimer()
    End Sub
    Public Sub EnableWifi()
        Try
            Dim commandModeWLAN As WLAN = New WLAN(FusionAccessType.COMMAND_MODE)
            If commandModeWLAN.Adapters.Item(0).PowerState = Adapter.PowerStates.OFF Then
                commandModeWLAN.Adapters.Item(0).PowerState = Adapter.PowerStates.ON
            End If
            commandModeWLAN.Dispose()
        Catch ex As Exception
        Finally

        End Try
    End Sub
End Module