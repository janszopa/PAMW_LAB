using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AjaxFormModel : PageModel
{
    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Email { get; set; }

    public JsonResult OnPost()
    {
        // Przetwarzanie danych
        var message = $"Dziękujemy, {Name}! Twój adres email: {Email} został zapisany.";

        // Zwracamy dane w formacie JSON
        return new JsonResult(new { message });
    }
}
