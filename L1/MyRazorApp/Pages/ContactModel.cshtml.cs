using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class ContactModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "Imię jest wymagane.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Imię musi mieć co najmniej 3 znaki.")]
    public string Name { get; set; }

    [BindProperty]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email.")]
    public string Email { get; set; }

    [BindProperty]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Wiadomość musi mieć co najmniej 6 znaków.")]
    public string Message { get; set; }

    public bool MessageSubmitted { get; set; } = false;

    public void OnGet()
    {
        // Domyślny widok dla żądań GET
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Przekazywanie danych przez TempData
        TempData["Name"] = Name;
        TempData["Email"] = Email;
        TempData["Message"] = Message;

        // Przekierowanie na nową stronę
        return RedirectToPage("Confirmation");
    }
}
