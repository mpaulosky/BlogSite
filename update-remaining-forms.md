# Remaining Files to Update

## Form Input Pattern (Most Common)
Replace Bootstrap classes with TailwindCSS equivalents:

### Bootstrap → TailwindCSS Mappings:

**Form Containers:**
- `<div class="row">` → `<div class="max-w-2xl">`
- `<div class="col-md-4">` / `<div class="col-lg-6">` / `<div class="col-xl-6">` → Remove (handled by parent)

**Form Fields:**
- `<div class="form-floating mb-3">` → `<div class="mb-4">`
- `<label for="X" class="form-label">` → `<label for="X" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">`
- `class="form-control"` → `class="w-full px-4 py-3 rounded-lg border border-gray-300 dark:border-gray-600 bg-white dark:bg-gray-700 text-gray-900 dark:text-white focus:ring-2 focus:ring-indigo-500 dark:focus:ring-indigo-400 focus:border-transparent placeholder-gray-400 dark:placeholder-gray-500 transition-all duration-200"`

**Validation:**
- `class="text-danger"` → `class="text-red-600 dark:text-red-400 text-sm mt-1"`
- `<ValidationSummary class="text-danger"` → `<ValidationSummary class="bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 text-red-800 dark:text-red-200 rounded-lg p-4 mb-4"`

**Buttons:**
- `class="w-100 btn btn-lg btn-primary"` → `class="w-full px-6 py-3 rounded-lg font-medium bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-700 hover:to-purple-700 text-white shadow-md hover:shadow-lg transform hover:scale-[1.02] active:scale-[0.98] transition-all duration-200"`
- `class="btn btn-primary"` → `class="px-6 py-3 rounded-lg font-medium bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-700 hover:to-purple-700 text-white shadow-md hover:shadow-lg transform hover:scale-105 active:scale-95 transition-all duration-200"`
- `class="btn btn-danger"` → `class="px-6 py-3 rounded-lg font-medium bg-gradient-to-r from-red-600 to-red-700 hover:from-red-700 hover:to-red-800 text-white shadow-md hover:shadow-lg transform hover:scale-105 active:scale-95 transition-all duration-200"`
- `class="btn btn-link"` → `class="text-indigo-600 dark:text-indigo-400 hover:text-indigo-700 dark:hover:text-indigo-300 underline transition-colors"`

**Headings:**
- `<h1>` → `<h1 class="text-4xl font-bold text-gray-900 dark:text-white mb-2">`
- `<h2>` → `<h2 class="text-2xl font-semibold text-gray-900 dark:text-white mb-2">`
- `<h3>` → `<h3 class="text-xl font-semibold text-gray-900 dark:text-white mb-4">`
- `<hr />` → Remove or `<div class="border-t border-gray-200 dark:border-gray-700 my-6"></div>`

## Files Requiring Updates

### Account/Pages (Priority Order):
1. ✅ Login.razor
2. ✅ Register.razor
3. ForgotPassword.razor
4. ResetPassword.razor  
5. ResendEmailConfirmation.razor
6. LoginWith2fa.razor
7. LoginWithRecoveryCode.razor
8. ConfirmEmail.razor
9. ForgotPasswordConfirmation.razor
10. RegisterConfirmation.razor
11. ExternalLogin.razor
12. AccessDenied.razor
13. Lockout.razor
14. ConfirmEmailChange.razor
15. InvalidPasswordReset.razor
16. InvalidUser.razor
17. ResetPasswordConfirmation.razor

### Account/Pages/Manage (Priority Order):
1. ✅ Index.razor
2. ChangePassword.razor
3. Email.razor
4. TwoFactorAuthentication.razor
5. ExternalLogins.razor
6. DeletePersonalData.razor
7. SetPassword.razor
8. PersonalData.razor
9. EnableAuthenticator.razor
10. GenerateRecoveryCodes.razor
11. ResetAuthenticator.razor
12. Disable2fa.razor

## Quick Reference - Modern Form Template

```razor
<div class="max-w-2xl mx-auto py-8">
    <h3 class="text-xl font-semibold text-gray-900 dark:text-white mb-4">Title</h3>
    <StatusMessage />
    
    <EditForm Model="Input" FormName="formname" OnValidSubmit="OnSubmit" method="post">
        <DataAnnotationsValidator />
        <ValidationSummary class="bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 text-red-800 dark:text-red-200 rounded-lg p-4 mb-4" role="alert" />
        
        <div class="mb-4">
            <label for="InputField" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                Label
            </label>
            <InputText @bind-Value="Input.Field" 
                       id="InputField" 
                       class="w-full px-4 py-3 rounded-lg border border-gray-300 dark:border-gray-600 
                              bg-white dark:bg-gray-700 text-gray-900 dark:text-white
                              focus:ring-2 focus:ring-indigo-500 dark:focus:ring-indigo-400 focus:border-transparent
                              placeholder-gray-400 dark:placeholder-gray-500
                              transition-all duration-200" 
                       placeholder="Placeholder" />
            <ValidationMessage For="() => Input.Field" class="text-red-600 dark:text-red-400 text-sm mt-1" />
        </div>
        
        <button type="submit" 
                class="w-full px-6 py-3 rounded-lg font-medium
                       bg-gradient-to-r from-indigo-600 to-purple-600 
                       hover:from-indigo-700 hover:to-purple-700
                       text-white
                       shadow-md hover:shadow-lg
                       transform hover:scale-[1.02] active:scale-[0.98]
                       transition-all duration-200">
            Submit
        </button>
    </EditForm>
</div>
```
