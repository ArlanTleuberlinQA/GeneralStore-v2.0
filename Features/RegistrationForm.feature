# Feature: User Registration
#     As a user
#     I want to be able to register in the General Store app
#     So that I can shop for products

# Background:
#     Given the user is on the registration page

# Scenario: Verify country selection text
#     When the user views the country selection text
#     Then the country selection text should be "Select the country where you want to shop"

# Scenario: Select country from dropdown
#     When the user selects country "France"
#     Then the selected country should be "France"

# Scenario: Verify background image
#     Then the background image should be visible

# Scenario: Verify toolbar title
#     Then the toolbar title should be "General Store"

# Scenario: Select gender radio buttons
#     When the user selects gender "female"
#     Then the "female" radio button should be selected
#     When the user selects gender "male"
#     Then the "male" radio button should be selected

# Scenario: Login with empty name shows error
#     When the user clicks the Let's Shop button
#     Then an error toast message should appear saying "Please enter your name"
#     Then the toolbar title should be "General Store"

# Scenario: Successful login with female gender
#     When the user enters name "Test User"
#     When the user selects gender "female"
#     When the user clicks the Let's Shop button
#     Then the user should be on the products page