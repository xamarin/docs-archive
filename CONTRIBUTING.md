# Contributing

Thank you for taking the time to contribute, we appreciate your help in helping the Xamarin community to learn and grow! All content submitted **must** be your own work.

The information below guides you through the organization of this repository and advises of the structure needed for naming folders, and how your recipe directory should be organized.

Please also visit the articles in the [Contributor guide](/contributor-guide), which gives detailed and practical help with the following:

* [Content Layout](/contributor-guide/content-layout.md)
* [Style Guide](/contributor-guide/style-guide.md)
* [Markdown and metadata](/contributor-guide/markdown-and-metadata.md)
* [Templates](/contributor-guide/recipe-templates)

# Process for contributing

## Small changes & edits

To make corrections and small updates follow these steps:

1. Fork the xamarin/recipes repo.

2. Create a branch for your changes.

3. Write your content. Refer to the template and style guide.

4. Submit a Pull Request (PR) from your branch to xamarin/recipes/master.

5. Make any necessary updates to your branch as discussed with the team via the PR. The maintainers will merge your PR once feedback has been applied and your change looks good.

If your PR is addressing an existing issue, add the Fixes #Issue_Number keyword to the commit message or PR description, so the issue can be automatically closed when the PR is merged. For more information, see Closing issues via commit messages.

## Big changes or new content

For large contributions and new content, [open an issue](https://github.com/xamarin/recipes/issues/new) describing the article you wish to write and how it relates to existing content. The content inside the docs folder is organized into sections that are organized by product area (e.g. android and ios). Try to determine the correct folder for your new content.

Get feedback on your proposal via the issue before starting to write.

If it's a new topic, you can use the [template file](contributor-guide/recipe-templates/README.md) as your starting point. 

The actual submission steps are the same as for small changes.

The Xamarin team will review your PR and let you know (via PR feedback) if the change looks good or if there are any other updates/changes necessary in order to approve it.

The maintainers will then merge your PR once feedback has been applied and your change looks good.

## Recipe Contents 

Each recipe that is submitted should include the following: 

* A markdown file named **README.md** containing recipe instructions.
* Recipe sample code. This should be placed inside a folder named **source**.
* Images. These should be placed inside a folder named **images**.

## Repository Structure

All recipes are contained within with `\Recipes` directory. 

The repository structure will look like the following: 

```
\Recipes 
    \xamarin-forms
        \maps 
            \recipe-name-here 
                README.md 
                \images 
                \source 
```

## Folder naming standards 

The folder name will be used as the URL slug and therefore it is important to use the suggestions below. This ensure it will be SEO optimized and relevant for other users: 

* Try to keep the folder name between 1-3 words.  
* If more than one word, use hyphens (-), rather than underscores(_) 
* Use only lowercase letters, numbers, and hyphens 
* Make sure it is relevant to the recipe.

