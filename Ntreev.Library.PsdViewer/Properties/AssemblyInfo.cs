//Released under the MIT License.
//
//Copyright (c) 2018 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
// 어셈블리와 관련된 정보를 수정하려면
// 이 특성 값을 변경하십시오.
[assembly: AssemblyTitle("Ntreev.Library.PsdViewer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyProduct("Ntreev.Library.PsdViewer")]

// ComVisible을 false로 설정하면 이 어셈블리의 형식이 COM 구성 요소에 
// 표시되지 않습니다.  COM에서 이 어셈블리의 형식에 액세스하려면 
// 해당 형식에 대해 ComVisible 특성을 true로 설정하십시오.
[assembly: ComVisible(false)]

//지역화 가능 응용 프로그램 빌드를 시작하려면 
//.csproj 파일에서 <PropertyGroup> 내에 <UICulture>CultureYouAreCodingWith</UICulture>를
//설정하십시오.  예를 들어 소스 파일에서 영어(미국)를
//사용하는 경우 <UICulture>를 en-US로 설정합니다.  그런 다음 아래
//NeutralResourceLanguage 특성의 주석 처리를 제거합니다.  아래 줄의 "en-US"를 업데이트하여
//프로젝트 파일의 UICulture 설정과 일치시킵니다.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //테마별 리소스 사전의 위치
    //(페이지 또는 응용 프로그램 리소스 사전에 
    // 리소스가 없는 경우에 사용됨)
    ResourceDictionaryLocation.SourceAssembly //제네릭 리소스 사전의 위치
    //(페이지, 응용 프로그램 또는 모든 테마별 리소스 사전에 
    // 리소스가 없는 경우에 사용됨)
)]
