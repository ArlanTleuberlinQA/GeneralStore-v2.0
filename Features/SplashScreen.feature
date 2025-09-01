@SplashScreen
# language: en
Feature: Splash Screen тесты
  Проверка базовых элементов при открытии приложения
 
  Scenario: Проверка отображения Splash экрана
    When application is launched
    Then Splash screen is displayed
    Then Splash screen is enabled
    Then On the Splash screen there is no text