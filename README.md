huawei AREngineDemo for Unity2020
======


arengiene 是 huawei 推出类似 google arcore 和 apple arkit的增强现实框架。arengine 使用默认的 builtin 渲染预览流， 这里改成 urp渲染管线。

大概的思路就是原来在CameraRender里使用的commandbuffer来渲染预览流， 这里在urp中使用 一个自定义的 RenderFeature里去画。