# language: en
Feature: Splash Screen тесты
  Проверка базовых элементов при открытии приложения
 
  Scenario: Проверка отображения Splash экрана
    When приложение запущено
    Then Splash экран отображается
    And Splash экран активен
    And на Splash экране нет текста