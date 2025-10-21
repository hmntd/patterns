# Вікторина — Laravel + Vue3

## Опис проекту

Проект «Вікторина» — це веб-додаток для перевірки знань користувачів за різними категоріями (наприклад, історія, географія, біологія тощо).  

Основний функціонал:  
- Реєстрація та авторизація користувачів  
- Створення та редагування категорій, питань та відповідей  
- Проходження вікторин з підрахунком результатів  
- Перегляд таблиці лідерів  
- Зміна налаштувань користувача  

Frontend: **Vue3 + TailwindCSS + shadcn-ui**  
Backend: **Laravel 10 + PostgreSQL**  

---

## Реалізація SOLID принципів

У проєкті дотримано принципів SOLID на рівні моделей, контролерів та компонентів Vue.  

### **S — Single Responsibility Principle (SRP)**

Кожен клас або компонент відповідає лише за **одну задачу**:

- **Моделі Laravel**
  - `Category.php` — керування категоріями, зв’язок з питаннями та спробами вікторини.  
  - `Question.php` — керування питаннями, зв’язок з відповідями.  
  - `QuizAttempt.php` — зберігання спроб користувача.  
  - `AttemptAnswers.php` — зберігання відповідей користувача.  
- **Vue компоненти**
  - `CategoryModalContent.vue` — відображення та редагування категорії та пов’язаних питань/відповідей.  
  - `CategoryCard.vue` — відображення категорії, назви, опису та статусу "Passed".  

> Перевірити: `app/Models` та `resources/js/Components`. Кожен клас/компонент вирішує тільки одну задачу.

---

### **O — Open/Closed Principle (OCP)**

Класи відкриті для розширення, але закриті для модифікації:

- В `Category.php` доданий аксесор `hasPassed`, який можна розширювати без зміни решти логіки.  
- Vue компоненти використовують props та emits (`@created`, `@closed`), що дозволяє розширювати поведінку без редагування коду.  

> Перевірити: `Category.php` → метод `hasPassed`; `CategoryModalContent.vue` → props та events.

---

### **L — Liskov Substitution Principle (LSP)**

- Моделі наслідують базовий `Model` Laravel (`Category`, `Question`, `QuizAttempt`), які можна взаємозамінювати у колекціях та зв’язках Eloquent.  
- Компоненти Vue (`<Input>`, `<Checkbox>`, `<Textarea>`) можна використовувати взаємозамінно у формах завдяки уніфікованому інтерфейсу `v-model`.  

> Перевірити: `resources/js/ui/*` → компоненти вводу.

---

### **I — Interface Segregation Principle (ISP)**

- Використано спеціалізовані props та emits для Vue компонентів, щоб не перевантажувати інтерфейс зайвими даними.  
- Laravel Interfaces:
  - Для аутентифікації та авторизації: `Authenticatable`, `Authorizable`.  
  - Також створені **власні Interfaces**, які реалізуються у Repositories для зберігання бізнес-логіки (наприклад, `CategoryRepositoryInterface`, `QuestionRepositoryInterface`). Контролери взаємодіють з цими репозиторіями, а не з моделями напряму.  

> Перевірити: `app/Repositories/*Interface.php` та `app/Repositories/*Repository.php`, `CategoryController.php`.

---

### **D — Dependency Inversion Principle (DIP)**

- Контролери залежать від **абстракцій (Interfaces / Repositories)**, а не від конкретних моделей чи бази даних.  
- Vue компоненти використовують **composition API** (`ref`, `reactive`, `watch`) та залежності (`useForm`, `usePage`) через ін’єкцію, а не глобальні змінні.  

> Перевірити: `CategoryController.php` → усі методи працюють через Repositories, `CategoryModalContent.vue` → залежить від `useForm` та `usePage`.

---

## Перевірка SOLID

- Відкрити модель → SRP, DIP  
- Відкрити `CategoryModalContent.vue` → SRP, OCP, ISP  
- Подивитися аксесори та props → OCP  
- Подивитися використання Interfaces та Repositories → DIP, ISP  
- Наслідування моделей → LSP  

---

## Як запустити проект

```bash
# Встановлення залежностей
composer install
npm install

# Налаштування .env
cp .env.example .env
php artisan key:generate

# Міграції та сидинг
php artisan migrate --seed

# Запуск backend
php artisan serve

# Запуск frontend
npm run dev

