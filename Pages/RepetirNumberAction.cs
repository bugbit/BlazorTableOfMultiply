namespace BlazorTableOfMultiply.Pages;

public class RepetirNumberAction
{
    private readonly Tabla _tabla;
    private readonly int _m;

    public RepetirNumberAction(Tabla tabla, int m)
    {
        _tabla = tabla;
        _m = m;
    }

    public async Task DoAction() => await _tabla.DoRepetirNumber(_m);
}
