# Publishing CSharpLearning.WebUIHelper to NuGet

## Варианты публикации

### 1. Публичный NuGet.org (Рекомендуется для открытого использования)

#### Шаги:
1. **Создайте аккаунт на NuGet.org**
   - Перейдите на https://www.nuget.org/
   - Зарегистрируйтесь или войдите

2. **Получите API ключ**
   - В профиле найдите раздел "API Keys"
   - Создайте новый ключ с правами на публикацию

3. **Настройте проект**
   ```bash
   # Обновите метаданные в .csproj файле
   <Authors>Ваше Имя</Authors>
   <Company>Ваша Компания</Company>
   <PackageProjectUrl>https://github.com/вашusername/csharp-learning-webui-helper</PackageProjectUrl>
   <RepositoryUrl>https://github.com/вашusername/csharp-learning-webui-helper</RepositoryUrl>
   ```

4. **Опубликуйте пакет**
   ```bash
   # Соберите пакет
   dotnet build --configuration Release
   dotnet pack --configuration Release
   
   # Опубликуйте
   dotnet nuget push "CSharpLearning.WebUIHelper/bin/Release/CSharpLearning.WebUIHelper.1.0.0.nupkg" --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
   ```

### 2. Приватный NuGet Feed (Для ограниченного доступа)

#### Вариант A: GitHub Packages
```bash
# Настройте nuget.config
dotnet nuget add source --username YOUR_USERNAME --password YOUR_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/YOUR_USERNAME/index.json"

# Опубликуйте
dotnet nuget push "CSharpLearning.WebUIHelper.1.0.0.nupkg" --source "github"
```

#### Вариант B: Azure DevOps Artifacts
```bash
# Настройте feed
dotnet nuget add source --name "MyAzureDevOps" --username "any" --password "YOUR_PAT" --store-password-in-clear-text "https://pkgs.dev.azure.com/YOUR_ORG/_packaging/YOUR_FEED/nuget/v3/index.json"

# Опубликуйте
dotnet nuget push "CSharpLearning.WebUIHelper.1.0.0.nupkg" --source "MyAzureDevOps"
```

#### Вариант C: Локальный файловый feed
```bash
# Создайте папку для feed
mkdir C:\NuGetFeed

# Скопируйте пакет
copy "CSharpLearning.WebUIHelper.1.0.0.nupkg" C:\NuGetFeed\

# Добавьте источник
dotnet nuget add source C:\NuGetFeed --name "LocalFeed"
```

## Использование пакета

### После публикации в NuGet.org:
```bash
dotnet add package CSharpLearning.WebUIHelper
```

### После настройки приватного feed:
```bash
dotnet add package CSharpLearning.WebUIHelper --source "YourFeedName"
```

## Версионирование

Для обновления версии измените в .csproj:
```xml
<Version>1.0.1</Version>  <!-- Patch -->
<Version>1.1.0</Version>  <!-- Minor -->
<Version>2.0.0</Version>  <!-- Major -->
```

## Рекомендации

1. **Для учебных проектов**: Используйте публичный NuGet.org
2. **Для корпоративного использования**: Используйте приватный feed
3. **Для локальной разработки**: Используйте файловый feed
4. **Всегда тестируйте** пакет перед публикацией
5. **Документируйте изменения** в README.md

## Troubleshooting

### Ошибка "Package already exists"
- Увеличьте версию в .csproj файле

### Ошибка "API key invalid"
- Проверьте правильность API ключа
- Убедитесь, что ключ имеет права на публикацию

### Ошибка "Source not found"
- Проверьте правильность URL источника
- Убедитесь, что источник добавлен: `dotnet nuget list source`
