# Вікторина — Реалізація порождаючих паттернів

У цьому проекті реалізовано три порождаючі паттерни:

---

## 1️⃣ Singleton — QuizScoreManager

**Файл:** `app/Services/QuizScoreManager.php` 
**Реалізація:** Єдиний сервіс для обчислення результатів вікторини та підрахунку балів користувача. Гарантує існування лише однієї інстанції в проекті.

---

## 2️⃣ Factory Method — QuestionFactory

**Файл:** `app/Service/QuestionFactory.php` 
**Реалізація:** Створення різних типів питань (`SingleChoiceQuestion` або `MultipleChoiceQuestion`) через централізований метод. Дозволяє легко додавати нові типи питань без змін у контролерах.

---

## 3️⃣ Builder — QuizBuilder

**Файл:** `app/Builders/QuizBuilder.php` 
**Реалізація:** Послідовне побудування повної вікторини з категорією, питаннями та відповідями. Дозволяє створювати складні структури даних у зручний та контрольований спосіб.

---

## Де перевірити реалізацію

- **Singleton** — `app/Services/QuizScoreManager.php` 
- **Factory Method** — `app/Services/QuestionFactory.php` 
- **Builder** — `app/Builders/QuizBuilder.php` 
- Контролери: `CategoryController.php`, `QuizController.php` 

