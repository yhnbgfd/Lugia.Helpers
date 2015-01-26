# ReadMe
样式资源文件

右键添加“Resource Dictionary”文件并编写控件样式

然后在App.xaml添加：
    <Application.Resources>  
        <ResourceDictionary>  
            <ResourceDictionary.MergedDictionaries>  
                <ResourceDictionary Source="Views/ResourceDictionarys/文件名字.xaml" />  
            </ResourceDictionary.MergedDictionaries>  
        </ResourceDictionary>  
    </Application.Resources>  
