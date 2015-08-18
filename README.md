===========================================Developer
-------s2quake@ntreev.comSummary
-------포토샵 파일을 분석해 필요한 정보를 사용할 수 있는 .net 용 라이브러리입니다..Net framework 3.5 기반으로 제작되었습니다. 때문에 Unity3D에서도 제약없이 사용이 가능합니다.라이브러리는 어도비에서 제공하는 포토샵 파일 포맷 정보 기반으로 제작되었습니다.http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/Image Resource IDs와 Additional Layer Information 부분은 종류가 워낙 많아서 
필요한 부분을 제외하고는 파싱 작업을 하지않았습니다.

라이브러리에서 제공하는 PsdViewer는 정보만 볼 수 있는 간단한 프로그램입니다. 
레이어의 그림내용을 보여주지는 않습니다.
작업의 목표는 쉬운 사용법, 모든 정보 추출, 빠른 속도입니다.Test Environment-------초반 : Photoshop cs3중후반 : Photoshop CCUsage
-------

SourceCode:

    using (PsdDocument document = PsdDocument.Create(filename))	{		foreach (var item in document.Childs)		{			Console.WriteLine("LayerName : " + item.Name);		}	}
License-------Ntreev Photoshop Document Parser for .NetReleased under the MIT License.Copyright (c) 2015 Ntreev Soft co., Ltd.Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.