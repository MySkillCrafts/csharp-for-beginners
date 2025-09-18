# 🚀 Настройка для разработки WebUIHelper

## Рекомендуемая структура для максимальной гибкости

### 1. Для студентов (стабильные версии)
```bash
# Студенты просто добавляют пакет
dotnet add package CSharpLearning.WebUIHelper
```

### 2. Для вашей разработки (гибкость)
```bash
# Используйте Project Reference для мгновенных изменений
dotnet add reference "path/to/CSharpLearning.WebUIHelper.csproj"
```

## Структура проекта

```
csharp-for-beginners-code/
├── WebUIHelper-NuGet/                    # NuGet пакет
│   ├── CSharpLearning.WebUIHelper/       # Основная библиотека
│   └── TestWebUIHelper/                  # Тесты пакета
├── Section 4 - Basics/
│   └── Lesson Projects/                  # Ваши уроки
└── Section 5 - Arrays/
    └── Lesson Projects/                  # Ваши уроки
```

## Workflow для разработки

### Шаг 1: Создайте общий solution
```bash
# В корне проекта
dotnet new sln -n "CSharpLearning"
dotnet sln add "WebUIHelper-NuGet/CSharpLearning.WebUIHelper/CSharpLearning.WebUIHelper.csproj"
dotnet sln add "Section 4 - Basics/01 - Introduction/CoffeeCalculator/CoffeeCalculator.csproj"
dotnet sln add "Section 5 - Arrays/01 - Arrays vs List/StudentGradeTracker/StudentGradeTracker.csproj"
```

### Шаг 2: Настройте Project References
```bash
# В каждом уроке
dotnet add reference "../../../WebUIHelper-NuGet/CSharpLearning.WebUIHelper/CSharpLearning.WebUIHelper.csproj"
```

### Шаг 3: Разработка
- Изменяете код в WebUIHelper
- Сразу видите изменения в уроках
- Не нужно пересобирать NuGet пакет

### Шаг 4: Публикация
- Когда готовы - публикуете новую версию на NuGet.org
- Студенты получают стабильную версию

## Преимущества этого подхода

✅ **Для студентов**: Простое подключение через NuGet  
✅ **Для вас**: Мгновенные изменения без пересборки  
✅ **Гибкость**: Можете тестировать новые функции  
✅ **Стабильность**: Студенты получают проверенные версии  

## Команды для быстрого старта

```bash
# 1. Создать общий solution
dotnet new sln -n "CSharpLearning"

# 2. Добавить все проекты
dotnet sln add "WebUIHelper-NuGet/CSharpLearning.WebUIHelper/CSharpLearning.WebUIHelper.csproj"
dotnet sln add "Section 5 - Arrays/01 - Arrays vs List/StudentGradeTracker/StudentGradeTracker.csproj"

# 3. Настроить references
cd "Section 5 - Arrays/01 - Arrays vs List/StudentGradeTracker"
dotnet add reference "../../../WebUIHelper-NuGet/CSharpLearning.WebUIHelper/CSharpLearning.WebUIHelper.csproj"

# 4. Запустить
dotnet run
```
