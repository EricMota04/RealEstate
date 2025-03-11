public enum RequestStatus
{
    Pending,   // Cliente envió la solicitud, el agente aún no responde
    Approved,  // El agente aceptó la solicitud y se creó un `Appointment`
    Rejected   // El agente rechazó la solicitud
}
